using System.ComponentModel.DataAnnotations;

namespace DrinkItUpWebApp.DAL.Entities
{
    public class Ingredient
    {

        public int IngredientId { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = null!;

        public int UnitId { get; set; }

        public Unit Unit { get; set; } = null!;

        public ICollection<DrinkIngredient> DrinkIngredients { get; set; } = new List<DrinkIngredient>();

    }
}
