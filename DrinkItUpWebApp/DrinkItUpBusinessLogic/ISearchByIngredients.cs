namespace DrinkItUpBusinessLogic
{
	public interface ISearchByIngredients
	{
		List<string> GetAllIngredientsMatchingNames(string input);

		List<string> GetAllNames();

		List<string> GetAllNamesFromSumbit(string input);
	}
}