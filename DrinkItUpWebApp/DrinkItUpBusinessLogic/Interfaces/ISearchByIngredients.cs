namespace DrinkItUpBusinessLogic.Interfaces
{
    public interface ISearchByIngredients
    {
        List<string> GetAllIngredientsMatchingNames(string input);

        List<string> GetAllNamesDistinct();

        List<string> GetAllNamesFromSumbit(string input);
    }
}