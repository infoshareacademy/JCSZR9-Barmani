namespace DrinkItUpBusinessLogic
{
	public interface ISearchByIngredients
	{
		List<string> GetAllIngredientsMatchingNames(string input);
	}
}