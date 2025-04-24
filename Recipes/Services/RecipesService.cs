using Microsoft.EntityFrameworkCore;
using Recipes.Entities;

namespace Recipes.Services
{
    public class RecipesService
    {
        private readonly AppDbContext dbContext;

        public RecipesService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Recipe>> GetAllRecipes()
        {
            var recipes = await dbContext.Recipe.ToListAsync();

            return recipes;
        }

        public async Task<Recipe> GetById(Guid id)
        {
            var recipe = await dbContext.Recipe.FindAsync(id);

            return recipe;
        }

        public async Task<Recipe> CreateRecipe(Recipe recipe)
        {
            await dbContext.Recipe.AddAsync(recipe);
            await dbContext.SaveChangesAsync();

            return recipe;
        }

        public async Task<Recipe> UpdateRecipe(Guid id, Recipe recipe)
        {
            if (id != recipe.Id)
            {
                throw new ArgumentException("Recipe ID mismatch");
            }

            dbContext.Recipe.Update(recipe);
            await dbContext.SaveChangesAsync();

            return recipe;
        }

        public async Task<Recipe> DeleteRecipe(Guid id)
        {
            var dbRecipe = await dbContext.Recipe.FindAsync(id);

            if (dbRecipe == null)
            {
                throw new KeyNotFoundException("Recipe not found");
            }

            dbContext.Recipe.Remove(dbRecipe);
            await dbContext.SaveChangesAsync();

            return dbRecipe;
        }   
    }
}
