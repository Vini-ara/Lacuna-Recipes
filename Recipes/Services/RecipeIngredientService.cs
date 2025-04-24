using Microsoft.EntityFrameworkCore;
using Recipes.Entities;
using Recipes.Dtos;

namespace Recipes.Services
{
    public class RecipeIngredientService
    {
        private readonly AppDbContext dbContext;

        public RecipeIngredientService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GetManyRecipeIngredientDto>> GetAllRecipeIngredientsByRecipeId(Guid recipeId)
        {
            var recipeIngredients = await dbContext.RecipeIngredient
                .Where(ri => ri.RecipeId == recipeId)
                .Include(ri => ri.Ingredient)
                .ToListAsync();

            var recipeIngredientsDto = recipeIngredients
              .Select(ri => new GetManyRecipeIngredientDto
              {
                  Id = ri.Id,
                  Ingredient = new GetOneIngredientDto
                  {
                      Id = ri.Ingredient.Id,
                      Name = ri.Ingredient.Name,
                      Unit = ri.Ingredient.Unit,
                  },
                  Ammount = ri.Ammount,
              })
              .ToList();

            return recipeIngredientsDto;
        }

        public async Task<List<GetManyRecipeIngredientDto>> CreateManyRecipeIngredients(CreateRecipeIngredientDto[] recipeIngredientsDto, Guid recipeId)
        {
            var recipeIngredients = recipeIngredientsDto
                .Select(ri => new RecipeIngredient
                {
                    Id = Guid.NewGuid(),
                    RecipeId = recipeId,
                    IngredientId = ri.IngredientId,
                    Ammount = ri.Ammount,
                })
                .ToArray();

            await dbContext.RecipeIngredient.AddRangeAsync(recipeIngredients);
            await dbContext.SaveChangesAsync();

            var recipeIngredientsResult = await this.GetAllRecipeIngredientsByRecipeId(recipeId);

            return recipeIngredientsResult;
        }

        public async Task<Recipe> DeleteManyRecipeIngredientsByRecipe(Recipe recipe)
        {
            dbContext.RecipeIngredient.RemoveRange(dbContext.RecipeIngredient
                .Where(ri => ri.RecipeId == recipe.Id));
            await dbContext.SaveChangesAsync();

            return recipe;
        }
    }
}
