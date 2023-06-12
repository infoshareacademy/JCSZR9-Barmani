using DrinkItUpBusinessLogic.DTOs;
using DrinkItUpBusinessLogic.Interfaces;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;

namespace DrinkItUpBusinessLogic
{
	public class ByCategoryService : IByCategoryService
	{
		private readonly IDrinkRepository _repository;

		public ByCategoryService(IDrinkRepository drinkRepository)
		{
			_repository = drinkRepository;
		}

		public Task<DrinkDto> GetDrinksByDifficultyId(int difficultyId)
		{
			throw new NotImplementedException();
		}

		public async Task<DrinkDto> GetDrinksByMainAlcoholId(int mainAlcoholId)
		{
			throw new NotImplementedException();
		}

	}
}
