using AutoMapper;
using DrinkItUpBusinessLogic.DTOs;
using DrinkItUpWebApp.DAL.Entities;

namespace DrinkItUpWebApp.MapperProfile
{
	public class MapperProfile : Profile
	{
		public MapperProfile()
		{
			CreateMap<Unit, UnitDto>();
			CreateMap<Ingredient, IngredientDto>();

		}
	}
}
