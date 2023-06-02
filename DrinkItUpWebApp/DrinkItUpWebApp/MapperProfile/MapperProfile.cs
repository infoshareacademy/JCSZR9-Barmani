using AutoMapper;
using DrinkItUpBusinessLogic.DTOs;
using DrinkItUpWebApp.DAL.Entities;
using DrinkItUpWebApp.Models;

namespace DrinkItUpWebApp.MapperProfile
{
	public class MapperProfile : Profile
	{
		public MapperProfile()
		{
			CreateMap<Unit, UnitDto>();
			CreateMap<Ingredient, IngredientDto>();
			CreateMap<Drink, DrinkDto>();
			CreateMap<DrinkDto, DrinkSearchModel>();
			CreateMap<Difficulty, DifficultyDto>();
			CreateMap<MainAlcohol, MainAlcoholDto>();

			CreateMap<Drink,DrinkWithDetailsDto>();

			CreateMap<DifficultyDto, DifficultyModel>();
			CreateMap<MainAlcoholDto, MainAlcoholModel>();
			CreateMap<UnitDto, UnitModel>();
			CreateMap<IngredientDto, IngredientModel>();
			CreateMap<DrinkWithDetailsDto, DrinkWithDetailsModel>();
		}
	}
}
