namespace DrinkItUpBusinessLogic.DTOs
{
    public class IngredientDto
    {
        public int IngredientId { get; set; }

        public string Name { get; set; } = null!;

        public decimal Quantity { get; set;}

        public UnitDto Unit { get; set; } = null!;

        public bool IsUsed { get; set; } = false;
    }
}
