using DrinkItUpWebApp.DAL.Repositories;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DrinkItUpBusinessLogic
{
    public class SearchByIngredients
    {
        private IngredientRepository _ingredientRepository;

        public SearchByIngredients(IngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        public List<string> GetAllIngredientsMatchingNames(string input)
        {
            var queryAllIngredientsNames = _ingredientRepository.GetAll()
                .Select(i => i.Name)
                .ToList();
            string sPattern = $"([^a-z]|^){input}([A-Z]|[a-z])*";
            
            var regex = new Regex(sPattern, RegexOptions.IgnoreCase);
            var listMatchingNames = queryAllIngredientsNames.Where(s => regex.IsMatch(s)).Take(5).ToList();

            return listMatchingNames;
        }
    }
}