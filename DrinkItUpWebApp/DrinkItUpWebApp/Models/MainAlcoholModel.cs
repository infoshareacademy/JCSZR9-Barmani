namespace DrinkItUpWebApp.Models
{
    public class MainAlcoholModel
    {
        public int MainAlcoholId { get; set; }
        public string Name { get; set; }
        public bool IsUsed { get; set; } = false;
        public List<MainAlcoholModel> MainAlcohols { get; set;}
    }
}
