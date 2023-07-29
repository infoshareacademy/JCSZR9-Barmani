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

			CreateMap<UnitModel, UnitDto>();
			CreateMap<UnitDto, Unit>();

            CreateMap<MainAlcoholModel, MainAlcoholDto>();
            CreateMap<MainAlcoholDto, MainAlcohol>();

            CreateMap<DifficultyModel, DifficultyDto>();
            CreateMap<DifficultyDto, Difficulty>();

			CreateMap<IngredientModel, IngredientDto>();
			CreateMap<IngredientDto, Ingredient>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<Role, RoleDto>();
            CreateMap<RoleDto, Role>();

			CreateMap<DrinkWithDetailsDto, Drink>();
			CreateMap<Drink,DrinkWithDetailsDto>();


        }
	}
}
