using Microsoft.AspNetCore.Mvc;
using Recipes.Services;
using Recipes.Dtos;

namespace Recipes.Controllers
{
    [Route("api/ingredients")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IngredientsService ingredientsService;

        public IngredientsController(IngredientsService ingredientsService)
        {
            this.ingredientsService = ingredientsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllIngredients()
        {
            var ingredients = await ingredientsService.GetAllIngredients();

            return Ok(ingredients);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetIngredientById(Guid id)
        {
            var ingredient = await ingredientsService.GetById(id);

            return Ok(ingredient);
        }

        [HttpPost]
        public async Task<IActionResult> CreateIngredient([FromBody] CreateIngredientDto ingredient)
        {
            var createdIngredient = await ingredientsService.CreateIngredient(ingredient);

            return CreatedAtAction(nameof(GetIngredientById), new { id = createdIngredient.Id }, createdIngredient);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateIngredient(Guid id, [FromBody] UpdateIngredientDto ingredient)
        {
            var updatedIngredient = await ingredientsService.UpdateIngredient(id, ingredient);

            return Ok(updatedIngredient);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteIngredient(Guid id)
        {
            var deletedIngredient = await ingredientsService.DeleteIngredient(id);

            return Ok(deletedIngredient);
        }
    }
}
