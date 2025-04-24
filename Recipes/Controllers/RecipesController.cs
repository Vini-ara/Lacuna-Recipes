using Microsoft.AspNetCore.Mvc;
using Recipes.Services;
using Recipes.Entities;

namespace Recipes.Controllers
{
    [Route("api/recipes")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly RecipesService recipesService;

        public RecipesController(RecipesService recipesService)
        {
            this.recipesService = recipesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRecipes()
        {
            var recipes = await recipesService.GetAllRecipes();

            return Ok(recipes);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetRecipeById(Guid id)
        {
            var recipe = await recipesService.GetById(id);

            if (recipe == null)
            {
                return NotFound();
            }

            return Ok(recipe);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecipe([FromBody] Recipe recipe)
        {
            var createdRecipe = await recipesService.CreateRecipe(recipe);

            return CreatedAtAction(nameof(GetRecipeById), new { id = createdRecipe.Id }, createdRecipe);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateRecipe(Guid id, [FromBody] Recipe recipe)
        {
            var updatedRecipe = await recipesService.UpdateRecipe(id, recipe);

            return Ok(updatedRecipe);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteRecipe(Guid id)
        {
            var deletedRecipe = await recipesService.DeleteRecipe(id);

            return Ok(deletedRecipe);
        }
    }
}
