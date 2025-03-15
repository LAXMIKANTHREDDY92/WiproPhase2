using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using RecipeManagementAPI.Models;
using RecipeManagementAPI.DTOs;
using RecipeManagementAPI.Data;

namespace RecipeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // ================= REGISTER ====================
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto registerDto)
        {
            // Check if user already exists
            if (await _context.Users.AnyAsync(u => u.Username == registerDto.Username))
                return BadRequest("User already exists.");

            var hasher = new PasswordHasher<User>();

            var user = new User
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                Role = registerDto.Role ?? "User"
            };

            // Hash the password
            user.PasswordHash = hasher.HashPassword(user, registerDto.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully.");
        }

        // ================= LOGIN ====================
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            // Search by Username instead of Email
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == loginDto.Username);

            if (user == null)
                return Unauthorized("Invalid username or password.");

            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);

            if (result == PasswordVerificationResult.Failed)
                return Unauthorized("Invalid username or password.");

            var token = CreateJwtToken(user);
            return Ok(new { Token = token });
        }

        // =============== JWT GENERATOR ===============
        private string CreateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
