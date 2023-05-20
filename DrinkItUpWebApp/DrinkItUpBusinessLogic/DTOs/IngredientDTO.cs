namespace DrinkItUpBusinessLogic.DTOs
{
    public class IngredientDto
    {
        public int IngredientId { get; set; }

        public string Name { get; set; } = null!;

        public decimal Quantity { get; set; }

        public string Unit { get; set; } = null!;


    }
}
