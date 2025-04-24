using Microsoft.EntityFrameworkCore;
using Recipes.Entities;
using Recipes.Dtos;

namespace Recipes.Services
{
    public class RecipesService
    {
        private readonly AppDbContext dbContext;
        private readonly RecipeIngredientService recipeIngredientService;

        public RecipesService(AppDbContext dbContext, RecipeIngredientService recipeIngredientService)
        {
            this.dbContext = dbContext;
            this.recipeIngredientService = recipeIngredientService;
        }

        public async Task<List<GetManyRecipeDto>> GetAllRecipes()
        {
            var recipes = await dbContext.Recipe.ToListAsync();

            var recipeIngredients = await dbContext.RecipeIngredient
                .Include(ri => ri.Ingredient)
                .ToListAsync();

            var recipesDto = recipes
                .Select(r => new GetManyRecipeDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    RecipeIngredients = recipeIngredients
                        .Where(ri => ri.RecipeId == r.Id)
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
                        .ToList(),
                })
                .ToList();

            return recipesDto;
        }

        public async Task<GetOneRecipeDto> GetById(Guid id)
        {
            var recipe = await dbContext.Recipe.FindAsync(id);

            if (recipe == null)
            {
                throw new KeyNotFoundException("Recipe not found");
            }

            var recipeIngredients = await recipeIngredientService.GetAllRecipeIngredientsByRecipeId(id);

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

            var recipeDto = new GetOneRecipeDto
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Description = recipe.Description,
                RecipeIngredients = recipeIngredientsDto,
            };

            return recipeDto;
        }

        public async Task<GetOneRecipeDto> CreateRecipe(CreateRecipeDto createRecipeDto)
        {
            if (createRecipeDto.RecipeIngredients.Count == 0)
            {
                throw new ArgumentException("Recipe must have at least one ingredient");
            }

            var recipe = new Recipe
            {
                Id = Guid.NewGuid(),
                Name = createRecipeDto.Name,
                Description = createRecipeDto.Description,
            };

            await dbContext.Recipe.AddAsync(recipe);
            await dbContext.SaveChangesAsync();

            var recipeIngredients = createRecipeDto.RecipeIngredients
                .Select(ri => new CreateRecipeIngredientDto
                {
                    IngredientId = ri.IngredientId,
                    Ammount = ri.Ammount,
                })
                .ToArray();

            var createdRecipeIngredients = await recipeIngredientService.CreateManyRecipeIngredients(recipeIngredients, recipe.Id);

            var recipeDto = new GetOneRecipeDto
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Description = recipe.Description,
                RecipeIngredients = createdRecipeIngredients.ToList(),
            };

            return recipeDto;
        }

        public async Task<GetOneRecipeDto> UpdateRecipe(Guid id, UpdateRecipeDto updateRecipeDto)
        {
            var recipe = new Recipe
            {
                Id = id,
                Name = updateRecipeDto.Name,
                Description = updateRecipeDto.Description,
            };

            dbContext.Recipe.Update(recipe);
            await dbContext.SaveChangesAsync();

            await recipeIngredientService.DeleteManyRecipeIngredientsByRecipe(recipe);

            var recipeIngredients = updateRecipeDto.RecipeIngredients
                .Select(ri => new CreateRecipeIngredientDto
                {
                    IngredientId = ri.IngredientId,
                    Ammount = ri.Ammount,
                })
                .ToArray();

            var createdRecipeIngredients = await recipeIngredientService.CreateManyRecipeIngredients(recipeIngredients, recipe.Id);

            var recipeDto = new GetOneRecipeDto
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Description = recipe.Description,
                RecipeIngredients = createdRecipeIngredients.ToList(),
            };

            return recipeDto;
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
