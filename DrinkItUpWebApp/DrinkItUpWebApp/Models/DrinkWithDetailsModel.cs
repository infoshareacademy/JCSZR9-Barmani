using DrinkItUpBusinessLogic.DTOs;
using DrinkItUpWebApp.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace DrinkItUpWebApp.Models
{
	public class DrinkWithDetailsModel
	{
		public int DrinkId { get; set; }

		public string Name { get; set; } = null!;

		public MainAlcoholModel MainAlcohol { get; set; } = null!;

		public DifficultyModel Difficulty { get; set; } = null!;

		public string Description { get; set; } = null!;

		public List<IngredientModel> Ingredients { get; set; }

        public string DrinkPhotoId { get; set; }
    }
}
