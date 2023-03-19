namespace DrinkItUp.BusinessLogic.Model
{
    public class MainAlcohol
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public MainAlcohol(int id, string alcohol)
        {
            Id = id;
            Name = alcohol;
        }
    }
}