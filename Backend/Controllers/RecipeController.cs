using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeManagementAPI.Data;
using RecipeManagementAPI.DTOs; // Add this for RecipeCreateDto
using RecipeManagementAPI.Models;
using System.Security.Claims;

namespace RecipeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RecipeController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ GET: api/Recipe
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes()
        {
            var recipes = await _context.Recipes
                .Include(r => r.User)
                .Include(r => r.Category)
                .Select(r => new Recipe
                {
                    Id = r.Id,
                    Title = r.Title,
                    Description = r.Description,
                    Ingredients = r.Ingredients,
                    Instructions = r.Instructions,
                    ImageUrl = r.ImageUrl,
                    CategoryId = r.CategoryId,
                    Category = new Category
                    {
                        Id = r.Category.Id,
                        Name = r.Category.Name
                    },
                    User = new User
                    {
                        Id = r.User.Id,
                        Username = r.User.Username,
                        Email = r.User.Email
                    },
                    CreatedOn = r.CreatedOn,
                    UpdatedOn = r.UpdatedOn
                })
                .ToListAsync();

            return Ok(recipes);
        }


        // ✅ GET: api/Recipe/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipe(int id)
        {
            var recipe = await _context.Recipes
                .Include(r => r.Category)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (recipe == null)
                return NotFound();

            return recipe;
        }

        // ✅ POST: api/Recipe (Requires Authentication)
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Recipe>> CreateRecipe(RecipeCreateDto recipeDto)
        {
            // Get UserId from the JWT token
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "id");

            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            var recipe = new Recipe
            {
                Title = recipeDto.Title,
                Description = recipeDto.Description,
                Ingredients = recipeDto.Ingredients,
                Instructions = recipeDto.Instructions,
                ImageUrl = recipeDto.ImageUrl,
                CategoryId = recipeDto.CategoryId,
                UserId = userId,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };

            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRecipe), new { id = recipe.Id }, recipe);
        }

        // ✅ PUT: api/Recipe/5 (Optional Authentication depending on rules)
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateRecipe(int id, RecipeCreateDto recipeDto)
        {
            var recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null)
                return NotFound();

            // (Optional) Check if the logged-in user owns this recipe
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "id");

            if (userIdClaim == null)
                return Unauthorized();

            // Update fields
            recipe.Title = recipeDto.Title;
            recipe.Description = recipeDto.Description;
            recipe.Ingredients = recipeDto.Ingredients;
            recipe.Instructions = recipeDto.Instructions;
            recipe.ImageUrl = recipeDto.ImageUrl;
            recipe.CategoryId = recipeDto.CategoryId;
            recipe.UpdatedOn = DateTime.UtcNow;

            _context.Entry(recipe).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ✅ DELETE: api/Recipe/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null)
                return NotFound();

            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "id");

            if (userIdClaim == null)
                return Unauthorized();

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.Id == id);
        }
    }
}
