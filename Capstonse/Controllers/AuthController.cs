using Microsoft.AspNetCore.Mvc;
using RecipeManagementAPI.Data;
using RecipeManagementAPI.Models;
using RecipeManagementAPI.Services; // Ensure you have the correct namespace
using RecipeManagementAPI.ViewModels;

namespace RecipeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly JwtService _jwtService;

        public AuthController(AppDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        // ✅ Login Endpoint: Authenticate user and return JWT token
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginViewModel model)
        {
            // Validate user against the database
            var user = _context.Users.FirstOrDefault(u =>
                u.Email == model.Email && u.Password == model.Password);

            if (user == null)
                return Unauthorized("Invalid email or password");

            // Generate JWT Token
            var token = _jwtService.GenerateToken(user);
            return Ok(new { token });
        }

        // ✅ Register Endpoint: Allow user registration
        [HttpPost("register")]
        public IActionResult Register([FromBody] User newUser)
        {
            if (_context.Users.Any(u => u.Email == newUser.Email))
            {
                return BadRequest("Email already exists.");
            }

            _context.Users.Add(newUser);
            _context.SaveChanges();
            return Ok("User registered successfully.");
        }
    }
}
