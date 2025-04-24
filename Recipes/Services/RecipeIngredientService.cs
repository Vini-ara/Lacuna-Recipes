using Microsoft.EntityFrameworkCore;
using Recipes.Entities;

namespace Recipes.Services
{
    public class RecipeIngredientService
    {
        private readonly AppDbContext dbContext;

        public RecipeIngredientService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<RecipeIngredient>> GetAllRecipeIngredients()
        {
            var recipeIngredients = await dbContext.RecipeIngredient.ToListAsync();

            return recipeIngredients;
        }

        public async Task<List<RecipeIngredient>> GetAllRecipeIngredientsByRecipeId(Guid recipeId)
        {
            var recipeIngredients = await dbContext.RecipeIngredient
                .Where(ri => ri.RecipeId == recipeId)
                .ToListAsync();

            return recipeIngredients;
        }

        public async Task<RecipeIngredient> GetById(Guid id)
        {
            var recipeIngredient = await dbContext.RecipeIngredient.FindAsync(id);

            return recipeIngredient;
        }

        public async Task<RecipeIngredient> CreateRecipeIngredient(RecipeIngredient recipeIngredient)
        {
            await dbContext.RecipeIngredient.AddAsync(recipeIngredient);
            await dbContext.SaveChangesAsync();

            return recipeIngredient;
        }

        public async Task<RecipeIngredient> UpdateRecipeIngredient(Guid id, RecipeIngredient recipeIngredients)
        {
            if (id != recipeIngredients.Id)
            {
                throw new ArgumentException("Recipe ID mismatch");
            }

            dbContext.RecipeIngredient.Update(recipeIngredients);
            await dbContext.SaveChangesAsync();

            return recipeIngredients;
        }

        public async Task<RecipeIngredient> DeleteRecipeIngredient(Guid id)
        {
            var dbRecipe = await dbContext.RecipeIngredient.FindAsync(id);

            if (dbRecipe == null)
            {
                throw new KeyNotFoundException("Recipe not found");
            }

            dbContext.RecipeIngredient.Remove(dbRecipe);
            await dbContext.SaveChangesAsync();

            return dbRecipe;
        }
    }
}
