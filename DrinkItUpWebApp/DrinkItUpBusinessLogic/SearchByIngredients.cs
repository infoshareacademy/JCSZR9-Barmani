using DrinkItUpWebApp.DAL.Repositories;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DrinkItUpBusinessLogic
{
    public class SearchByIngredients
    {
        private IIngredientRepository _ingredientRepository;

        public SearchByIngredients(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        public List<string> GetAllIngredientsMatchingNames(string input)
        {
            var allIngredientsNames = _ingredientRepository.GetAll()
                .Select(i => i.Name)
                .ToList();

            string pattern = $"([^a-z]|^){input}([A-Z]|[a-z])*";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);

            var matchingNames = allIngredientsNames.Where(s => regex.IsMatch(s)).Take(5).ToList();

            return matchingNames;
        }
    }
}