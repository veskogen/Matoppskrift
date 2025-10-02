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
        public async Task<ActionResult<IEnumerable<RecipeReadDto>>> GetRecipes()
        {
            var recipes = await _context.Recipes
                .Include(r => r.Ingredients) // Include related ingredients
                .ToListAsync();

            var recipeDtos = recipes.Select(r => new RecipeReadDto
                {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                Instructions = r.Instructions,
                PrepTimeMinutes = r.PrepTimeMinutes,
                CookTimeMinutes = r.CookTimeMinutes,
                Ingredients = r.Ingredients.Select(i => new IngredientReadDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    Quantity = i.Quantity
                }).ToList()
                }).ToList();

            return recipeDtos;
        }




        // GET: api/recipes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeReadDto>> GetRecipe(int id)
        {
            var recipe = await _context.Recipes
                .Include(r => r.Ingredients)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (recipe == null)
            {
                return NotFound();
            }

            var recipeReadDto = new RecipeReadDto
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Description = recipe.Description,
                Instructions = recipe.Instructions,
                PrepTimeMinutes = recipe.PrepTimeMinutes,
                CookTimeMinutes = recipe.CookTimeMinutes,
                Ingredients = recipe.Ingredients.Select(i => new IngredientReadDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    Quantity = i.Quantity
                }).ToList()
            };

            return recipeReadDto;
        }



        // POST: api/recipes
        [HttpPost]
        public async Task<ActionResult<RecipeReadDto>> PostRecipe([FromBody] RecipeCreateDto recipeCreateDto)
        {
            // Simple validation (can be improved with Data Annotations/FluentValidation)
    if (string.IsNullOrWhiteSpace(recipeCreateDto.Name) || string.IsNullOrWhiteSpace(recipeCreateDto.Instructions))
    {
        return BadRequest("Recipe name and instructions are required.");
    }
            var recipe = new Recipe
            {
                Name = recipeCreateDto.Name,
                Description = recipeCreateDto.Description,
                PrepTimeMinutes = recipeCreateDto.PrepTimeMinutes,
                Instructions = recipeCreateDto.Instructions,
                CookTimeMinutes = recipeCreateDto.CookTimeMinutes,
                Ingredients = recipeCreateDto.Ingredients.Select(i => new Ingredient
                {
                    Name = i.Name,
                    Quantity = i.Quantity
                }).ToList()
            };
            // Ensure ingredients are properly linked if sent in the payload
            if (recipe.Ingredients != null)
            {
                foreach (var ingredient in recipe.Ingredients)
                {
                    ingredient.RecipeId = recipe.Id; // Ensure foreign key is set
                }
            }

            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            // Map Recipe to RecipeReadDto
            var recipeReadDto = new RecipeReadDto
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Description = recipe.Description,
                Instructions = recipe.Instructions,
                PrepTimeMinutes = recipe.PrepTimeMinutes,
                CookTimeMinutes = recipe.CookTimeMinutes,
                Ingredients = recipe.Ingredients.Select(i => new IngredientReadDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    Quantity = i.Quantity
                }).ToList()
            };

            // Returns 201 Created with the location of the new resource
            return CreatedAtAction("GetRecipe", new { id = recipe.Id }, recipeReadDto);
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