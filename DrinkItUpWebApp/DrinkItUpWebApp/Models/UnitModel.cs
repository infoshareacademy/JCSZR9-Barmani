namespace DrinkItUpWebApp.Models
{
    public class UnitModel
    {
        public int UnitId { get; set; }

        public string Name { get; set; }

        public List<UnitModel> Units { get; set; }

        public bool IsUsed { get; set; } = false;
    }
}
