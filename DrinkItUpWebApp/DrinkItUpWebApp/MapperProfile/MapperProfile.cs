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

		}
	}
}
