namespace Recipes.Dtos {
  public class CreateIngredientDto {
    public string Name { get; set; }
    public string Unit { get; set; }

  }

  public class UpdateIngredientDto {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Unit { get; set; }
  }

  public class GetOneIngredientDto {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Unit { get; set; }
  }
}
