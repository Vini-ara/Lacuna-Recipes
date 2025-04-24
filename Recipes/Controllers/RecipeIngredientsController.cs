using Microsoft.AspNetCore.Mvc;
using Recipes.Services;
using Recipes.Entities;

namespace Recipes.Controllers
{
    [Route("api/recipeIngredients")]
    [ApiController]
    public class RecipeIngredientsController : ControllerBase
    {
        private readonly RecipeIngredientService recipeIngredientService;

        public RecipeIngredientsController(RecipeIngredientService recipeIngredientsService)
        {
            this.recipeIngredientService = recipeIngredientsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRecipeIngredients()
        {
            var recipeIngredients = await recipeIngredientService.GetAllRecipeIngredients();

            return Ok(recipeIngredients);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetRecipeIngredientById(Guid id)
        {
            var recipeIngredient = await recipeIngredientService.GetById(id);

            if (recipeIngredient == null)
            {
                return NotFound();
            }

            return Ok(recipeIngredient);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecipeIngredient([FromBody] RecipeIngredient recipeIngredient)
        {
            var createdRecipeIngredient = await recipeIngredientService.CreateRecipeIngredient(recipeIngredient);

            return CreatedAtAction(nameof(GetRecipeIngredientById), new { id = createdRecipeIngredient.Id }, createdRecipeIngredient);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateRecipeIngredient(Guid id, [FromBody] RecipeIngredient recipeIngredient)
        {
            var updatedRecipeIngredient = await recipeIngredientService.UpdateRecipeIngredient(id, recipeIngredient);

            return Ok(updatedRecipeIngredient);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteRecipeIngredient(Guid id)
        {
            var deletedRecipeIngredient = await recipeIngredientService.DeleteRecipeIngredient(id);

            return Ok(deletedRecipeIngredient);
        }
    }
}
