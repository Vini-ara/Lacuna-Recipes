using Microsoft.EntityFrameworkCore;
using Recipes.Entities;
using Recipes.Dtos;

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

            if (ingredient == null)
            {
                throw new KeyNotFoundException("Ingredient not found");
            }

            return ingredient;
        }

        public async Task<Ingredient> CreateIngredient(CreateIngredientDto ingredientDto)
        {
            var ingredient = new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = ingredientDto.Name,
                Unit = ingredientDto.Unit
            };

            await dbContext.Ingredient.AddAsync(ingredient);
            await dbContext.SaveChangesAsync();

            return ingredient;
        }

        public async Task<Ingredient> UpdateIngredient(Guid id, UpdateIngredientDto ingredientDto)
        {
            if (id != ingredientDto.Id)
            {
                throw new ArgumentException("Ingredient ID mismatch");
            }

            var dbIngredient = await dbContext.Ingredient.FindAsync(id);

            if (dbIngredient == null)
            {
                throw new KeyNotFoundException("Ingredient not found");
            }

            var ingredient = new Ingredient
            {
                Id = id,
                Name = ingredientDto.Name,
                Unit = ingredientDto.Unit
            };

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
