using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeManagementAPI.Data;
using RecipeManagementAPI.Models;

namespace RecipeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Requires JWT authentication for all endpoints
    public class RecipesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RecipesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/recipes - Retrieve all recipes
        [HttpGet]
        public async Task<IActionResult> GetRecipes()
        {
            var recipes = await _context.Recipes.ToListAsync();
            return Ok(recipes);
        }

        // GET: api/recipes/{id} - Retrieve a recipe by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipeById(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null) return NotFound("Recipe not found");

            return Ok(recipe);
        }

        // POST: api/recipes - Create a new recipe
        [HttpPost]
        public async Task<IActionResult> CreateRecipe([FromBody] Recipe recipe)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRecipeById), new { id = recipe.Id }, recipe);
        }

        // PUT: api/recipes/{id} - Update an existing recipe
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecipe(int id, [FromBody] Recipe updatedRecipe)
        {
            if (id != updatedRecipe.Id) return BadRequest("Recipe ID mismatch");

            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null) return NotFound("Recipe not found");

            recipe.Title = updatedRecipe.Title;
            recipe.Ingredients = updatedRecipe.Ingredients;
            recipe.Instructions = updatedRecipe.Instructions;
            recipe.Category = updatedRecipe.Category;

            _context.Recipes.Update(recipe);
            await _context.SaveChangesAsync();
            return NoContent(); // 204 No Content on successful update
        }

        // DELETE: api/recipes/{id} - Delete a recipe
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null) return NotFound("Recipe not found");

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
