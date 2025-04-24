using System.ComponentModel.DataAnnotations;

namespace Recipes.Entities
{
    public class RecipeIngredient
    {
        [Key]
        public Guid Id { get; set; }

        public Guid RecipeId { get; set; }
        public Recipe Recipe { get; set; } = null!;

        public Guid IngredientId { get; set; }
        public Ingredient Ingredient { get; set; } = null!;

        [Required]
        public double Ammount { get; set; }
    }
}
