using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ShopTrackPro.Core.DTO;
using ShopTrackPro.Core.Entities;
using ShopTrackPro.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ShopTrackPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "auth")]
    public class AuthController : ControllerBase
    {
        private readonly ShopTrackProContext _context;
        private readonly IConfiguration _config;
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthController(ShopTrackProContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
            _passwordHasher = new PasswordHasher<User>();
        }

        // ================= REGISTER =================
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequest)
        {
            if (registerRequest == null
                || string.IsNullOrWhiteSpace(registerRequest.Email)
                || string.IsNullOrWhiteSpace(registerRequest.Password)
                || string.IsNullOrWhiteSpace(registerRequest.Role))
            {
                return BadRequest(new { message = "Invalid input" });
            }

            var existingUser = await _context.Users.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email.ToLower() == registerRequest.Email.ToLower());

            if (existingUser != null)
                return BadRequest(new { message = "Email already exists" });

            // ✅ Create user with role
            var user = new User
            {
                Name = registerRequest.Name,
                Email = registerRequest.Email,
                Role = registerRequest.Role // Role from DTO
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, registerRequest.Password);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User registered successfully" });
        }

        // ================= LOGIN =================
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
        {
            if (loginRequest == null || string.IsNullOrWhiteSpace(loginRequest.Email) || string.IsNullOrWhiteSpace(loginRequest.Password))
                return BadRequest(new { message = "Invalid input" });

            var user = await _context.Users.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email.ToLower() == loginRequest.Email.ToLower());

            if (user == null)
                return Unauthorized(new { message = "Invalid email or password" });

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginRequest.Password);
            if (result == PasswordVerificationResult.Failed)
                return Unauthorized(new { message = "Invalid email or password" });

            var token = GenerateJwtToken(user);

            return Ok(new AuthResponseDTO
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:ExpireMinutes"]))
            });
        }

        // ================= JWT GENERATION =================
        private string GenerateJwtToken(User user)
        {
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key is missing"));
            var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role) // ✅ Include role claim
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:ExpireMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
