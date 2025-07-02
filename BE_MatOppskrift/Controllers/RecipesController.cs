using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BE_MatOppskrift.Data;
using BE_MatOppskrift.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE_MatOppskrift.Controllers
{
    [ApiController] // Indicates that this class is an API controller
    [Route("api/[controller]")] // Sets the route for this controller

    public class RecipesController : ControllerBase
    {
        private readonly RecipeContext _context;

        // Constructor to inject the DbContext (EF Core context
        public RecipesController(RecipeContext context)
        {
            _context = context;
        }

        // GET: api/recipes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes()
        {
            return await _context.Recipes
                .Include(r => r.Ingredients) // Include related ingredients
                .ToListAsync();
        }
        // GET: api/recipes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipe(int id)
        {
            // Find a recipe by its ID, including related ingredients
            var recipe = await _context.Recipes
                .Include(r => r.Ingredients) // Include related ingredients
                .FirstOrDefaultAsync(r => r.Id == id);

            if (recipe == null)
            {
                return NotFound(); // Return 404 if recipe not found
            }

            return recipe; // Return 200 ok with the found recipe
        }
        // POST: api/recipes
        [HttpPost]
        public async Task<ActionResult<Recipe>> PostRecipe(Recipe recipe)
        {
            // Simple validation (can be improved with Data Annotations/FluentValidation)
            if (string.IsNullOrWhiteSpace(recipe.Name) || string.IsNullOrWhiteSpace(recipe.Instructions))
            {
                return BadRequest("Recipe name and instructions are required.");
            }
            // Ensure ingredients are properly linked if sent in the payload
            if (recipe.Ingredients != null)
            {
                foreach (var ingredient in recipe.Ingredients)
                {
                    ingredient.Recipe = recipe; // Link back to the recipe
                }
            }

            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            // Returns 201 Created with the location of the new resource
            return CreatedAtAction("GetRecipe", new { id = recipe.Id }, recipe);
        }
        // PUT: api/recipes/{id}
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipe(int id, Recipe recipe)
        {
            if (id != recipe.Id)
            {
                return BadRequest();
            }

            //mark the recipe as modified
            _context.Entry(recipe).State = EntityState.Modified;

            try
            {   // For nested objects like Ingredients, you need more sophisticated handling
                // For simplicity here, we assume Ingredients are updated separately or managed carefully.
                // A common pattern is to fetch the existing recipe, update its properties,
                // and then manage its related ingredients (add new, update existing, remove old).
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
         // DELETE: api/Recipes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound(); // Returns 404
            }

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return NoContent(); // Returns 204 No Content for successful deletion
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.Id == id);
        }
    }
}