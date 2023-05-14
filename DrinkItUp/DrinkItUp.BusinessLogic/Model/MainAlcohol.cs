using System.Text.Json.Serialization;

namespace DrinkItUp.BusinessLogic.Model
{
    
    public class MainAlcohol
    {
        public int Id { get; set; }

        public string Alcohol { get; set; }

        
        public MainAlcohol(int id, string alcohol)
        {
            Id = id;
            Alcohol = alcohol;
        }
    }
}