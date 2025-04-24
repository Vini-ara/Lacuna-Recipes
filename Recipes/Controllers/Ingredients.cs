using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Recipes.Data;
using Recipes.Services;

namespace Recipes.Controllers
{
    [Route("api/ingredients")]
    [ApiController]
    public class Ingredients : ControllerBase
    {
        private readonly IngredientsService ingredientsService;

        public Ingredients(IngredientsService ingredientsService)
        {
            this.ingredientsService = ingredientsService;
        }

        [HttpGet("{id:guid}")]
        public async Task<JsonResult> GetAllIngredients()
        {
            var ingredients = await ingredientsService.GetAllIngredients();

            return new JsonResult(ingredients);
        }
    }
}
