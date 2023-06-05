namespace DrinkItUpWebApp.Models
{
    public class DifficultyModel
    {
        public int DifficultyId { get; set; }
        public string Name { get; set; }

        public bool IsUsed { get; set; } = false;

        public List<DifficultyModel> Difficulties { get; set; }
    }
}
