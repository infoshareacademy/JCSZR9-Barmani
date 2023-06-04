using DrinkItUpBusinessLogic.DTOs;

namespace DrinkItUpBusinessLogic.Interfaces
{
    public interface ISearchByIngredients
    {
        Task<List<string>> GetAllIngredientsMatchingNames(string input);

        Task<List<string>> GetAllNamesDistinct();

        List<string> GetAllNamesFromSumbit(string input);

		Task<List<DrinkDto>> GetMatchingDrinksToIngredients(string input);

	}
}