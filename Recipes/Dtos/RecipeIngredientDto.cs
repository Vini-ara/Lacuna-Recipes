namespace Recipes.Dtos {
  public class CreateRecipeIngredientDto {
    public Guid IngredientId { get; set; }
    public double Ammount { get; set; }
  }

  public class GetManyRecipeIngredientDto {
    public Guid Id { get; set; }
    public GetOneIngredientDto Ingredient { get; set; }
    public double Ammount { get; set; }
  }
}
