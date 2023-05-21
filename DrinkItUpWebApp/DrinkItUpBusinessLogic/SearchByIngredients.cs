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