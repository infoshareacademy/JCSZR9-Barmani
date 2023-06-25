using AutoMapper;
using DrinkItUpBusinessLogic.DTOs;
using DrinkItUpBusinessLogic.Interfaces;
using DrinkItUpWebApp.DAL.Entities;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;

namespace DrinkItUpBusinessLogic
{
	public class ByCategoryService : IByCategoryService
	{
		private readonly IDrinkRepository _drinkRepository;
		private readonly IMapper _mapper;


		public ByCategoryService(IDrinkRepository drinkRepository, IMapper mapper)
		{

			_mapper = mapper;
			_drinkRepository = drinkRepository;

		}

		public async Task<IEnumerable<DrinkDto>> GetDrinksByDifficultyId(int difficultyId)
		{
			var drinks = await _drinkRepository.GetDrinksByDifficultyId(difficultyId);
			var drinksDto = new List<DrinkDto>();

			foreach(var drink in drinks)
			{
				var drinkDto = _mapper.Map<DrinkDto>(drink);
				drinksDto.Add(drinkDto);
			}

			return drinksDto;
		}

		public async Task<IEnumerable<DrinkDto>> GetDrinksByMainAlcoholId(int mainAlcoholId)
		{
			var drinks = await _drinkRepository.GetDrinksByMainAlcoholId(mainAlcoholId);
			var drinksDto = new List<DrinkDto>();

			foreach (var drink in drinks)
			{
				var drinkDto = _mapper.Map<DrinkDto>(drink);
				drinksDto.Add(drinkDto);
			}

			return drinksDto;
		}

	}
}
