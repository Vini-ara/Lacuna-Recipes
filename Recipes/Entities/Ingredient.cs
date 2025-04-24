using System.ComponentModel.DataAnnotations;

namespace Recipes.Entities
{
    public class Ingredient
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Unit { get; set; }
    }
}
