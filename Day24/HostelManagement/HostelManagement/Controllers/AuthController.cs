using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HostelManagement.Core.DTOs;
using HostelManagement.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace HostelManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private static List<User> _users = new();   // In-memory user store
        private readonly PasswordHasher<User> _passwordHasher = new();

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        // ✅ Register a new user
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequestDTO registerRequest)
        {
            if (_users.Any(u => u.Username == registerRequest.Username))
            {
                return BadRequest(new { message = "Username already exists" });
            }

            var user = new User
            {
                Username = registerRequest.Username,
                Role = registerRequest.Role ?? "Staff"
            };

            user.Password = _passwordHasher.HashPassword(user, registerRequest.Password);
            _users.Add(user);

            return Ok(new { message = "User registered successfully", role = user.Role });
        }

        // ✅ Login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDTO loginRequest)
        {
            var user = _users.FirstOrDefault(u => u.Username == loginRequest.username);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, loginRequest.password);
            if (result == PasswordVerificationResult.Failed)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            var token = GenerateJwtToken(user);

            return Ok(new
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:ExpireMinutes"]))
            });
        }

        // ✅ JWT generator
        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(ClaimTypes.Role, user.Role ?? "Staff"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:ExpireMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
