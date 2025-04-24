using Microsoft.EntityFrameworkCore;
using Recipes.Entities;

namespace Recipes.Services
{
    public class IngredientsService
    {
        private readonly AppDbContext dbContext;

        public IngredientsService(AppDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<List<Ingredient>> GetAllIngredients() 
        {
            var ingredients = await dbContext.Ingredient.ToListAsync();

            return ingredients;
        }

        public async Task<Ingredient> GetById(Guid id)
        {
            var ingredient = await dbContext.Ingredient.FindAsync(id);

            return ingredient;
        }

        public async Task<Ingredient> CreateIngredient(Ingredient ingredient)
        {
            await dbContext.Ingredient.AddAsync(ingredient);
            await dbContext.SaveChangesAsync();

            return ingredient;
        }

        public async Task<Ingredient> UpdateIngredient(Guid id, Ingredient ingredient)
        {
            if (id != ingredient.Id)
            {
                throw new ArgumentException("Ingredient ID mismatch");
            }

            dbContext.Ingredient.Update(ingredient);
            await dbContext.SaveChangesAsync();

            return ingredient;
        }

        public async Task<Ingredient> DeleteIngredient(Guid id)
        {
            var dbIngredient = await dbContext.Ingredient.FindAsync(id);

            if (dbIngredient == null)
            { 
                throw new KeyNotFoundException("Ingredient not found");
            }

            dbContext.Ingredient.Remove(dbIngredient);
            await dbContext.SaveChangesAsync();

            return dbIngredient;
        }
    } 
}
