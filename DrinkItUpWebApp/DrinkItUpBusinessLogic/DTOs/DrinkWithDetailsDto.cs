using DrinkItUpWebApp.DAL.Entities;

namespace DrinkItUpBusinessLogic.DTOs
{
    public class DrinkWithDetailsDto
    {
        public int DrinkId { get; set; }
        public string Name { get; set; }

        public MainAlcoholDto MainAlcohol { get; set; }

        public Difficulty Difficulty { get; set; }

        public string Description { get; set; }
    }
}