using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BugTrack.Core.DTOs;
using BugTrack.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace BugTrack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private static List<User> _users = new();
        private readonly PasswordHasher<User> _passwordHasher = new();
        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequestDTO registerRequest)
        {
            // ✅ Check if username already exists
            if (_users.Any(u => u.Username == registerRequest.username))
            {
                return BadRequest(new { message = "Username already exists" });
            }

            // ✅ Save plain password (⚠️ not secure)
            var user = new User
            {
                Username = registerRequest.username
            };
            user.Password = _passwordHasher.HashPassword(user, registerRequest.password);

            _users.Add(user);

            return Ok(new { message = "User registered successfully" });
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDTO loginRequest)
        {
            // 1️⃣ Find user by username
            var user = _users.FirstOrDefault(u => u.Username == loginRequest.username);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            // 2️⃣ Verify hashed password
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, loginRequest.password);
            if (result == PasswordVerificationResult.Failed)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            // 3️⃣ Generate JWT if valid
            var token = GenerateJwtToken(user.Username);

            return Ok(new
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:ExpireMinutes"]))
            });
        }


        private string GenerateJwtToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["jwt:Issuer"],
                audience: _config["jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["jwt:ExpireMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
