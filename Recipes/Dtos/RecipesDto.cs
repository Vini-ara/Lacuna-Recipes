namespace Recipes.Dtos {
  public class CreateRecipeDto {
    public string Name { get; set; }
    public string Description { get; set; }
    public List<CreateRecipeIngredientDto> RecipeIngredients { get; set; }
  }

  public class UpdateRecipeDto {
    public string Name { get; set; }
    public string Description { get; set; }
    public List<CreateRecipeIngredientDto> RecipeIngredients { get; set; }
  }

  public class GetOneRecipeDto {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<GetManyRecipeIngredientDto> RecipeIngredients { get; set; }
  }

  public class GetManyRecipeDto {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<GetManyRecipeIngredientDto> RecipeIngredients { get; set; }
  }
}

