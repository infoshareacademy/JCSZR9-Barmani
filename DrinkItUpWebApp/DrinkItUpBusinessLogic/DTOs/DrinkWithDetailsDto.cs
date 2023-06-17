using DrinkItUpWebApp.DAL.Entities;

namespace DrinkItUpBusinessLogic.DTOs
{
    public class DrinkWithDetailsDto
    {
        public int DrinkId { get; set; }
        public string Name { get; set; }

        public MainAlcoholDto MainAlcohol { get; set; }

        public DifficultyDto Difficulty { get; set; }

        public string Description { get; set; }

        public List<IngredientDto> Ingredients { get; set; } = new();

        public string DrinkPhotoId { get; set; }
    }
}