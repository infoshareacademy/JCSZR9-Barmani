using Microsoft.Identity.Client;

namespace DrinkItUpWebApp.Models
{
	public class DrinkSearchModel
	{
		public int DrinkId { get; set; }

		public string Name { get; set; }

		public MainAlcoholModel MainAlcohol { get; set; }

		public DifficultyModel Difficulty { get; set; }

		public string Description { get; set; }

		public Dictionary<string, List<DrinkSearchModel>> Results { get; set; }

        public string DrinkPhotoId { get; set; }
    }
}
