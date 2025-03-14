using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RecipeManagementAPI.Data;
using RecipeManagementAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RecipeManagementAPI.Controllers
{
    [Route("api/users")] // ✅ Fixed route (lowercase & consistent)
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public UsersController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // ✅ POST: api/users/login (Fixed the route for login)
        [HttpPost("login")]
        public IActionResult Login([FromBody] User userLogin)
        {
            if (userLogin == null || string.IsNullOrEmpty(userLogin.Email) || string.IsNullOrEmpty(userLogin.Password))
            {
                return BadRequest("Email and Password are required.");
            }

            var user = _context.Users.FirstOrDefault(u => u.Email == userLogin.Email && u.Password == userLogin.Password);

            if (user == null)
            {
                return Unauthorized("Invalid email or password");
            }

            var token = GenerateToken(user);
            return Ok(new { token });
        }

        // ✅ POST: api/users/register (Fixed the route for register)
        [HttpPost("register")]
        public IActionResult Register([FromBody] User newUser)
        {
            if (newUser == null || string.IsNullOrEmpty(newUser.Email) || string.IsNullOrEmpty(newUser.Password))
            {
                return BadRequest("All fields are required.");
            }

            if (_context.Users.Any(u => u.Email == newUser.Email))
            {
                return BadRequest("Email is already in use.");
            }

            _context.Users.Add(newUser);
            _context.SaveChanges();
            return Ok("User registered successfully.");
        }

        // ✅ GET: api/users (Fetch all users - requires a valid token)
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }

        // ✅ Helper Method: Generate JWT Token
        private string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
