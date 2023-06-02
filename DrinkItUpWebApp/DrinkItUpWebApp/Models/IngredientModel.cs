using DrinkItUpBusinessLogic.DTOs;

namespace DrinkItUpWebApp.Models
{
    public class IngredientModel
    {
        public int IngredientId { get; set; }

        public string Name { get; set; } = null!;

        public decimal Quantity { get; set; }

        public UnitModel Unit { get; set; } = null!;
    }
}
