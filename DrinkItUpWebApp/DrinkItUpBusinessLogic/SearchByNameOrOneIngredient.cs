using AutoMapper;
using DrinkItUpBusinessLogic.DTOs;
using DrinkItUpBusinessLogic.Interfaces;
using DrinkItUpWebApp.DAL.Entities;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;


namespace DrinkItUpBusinessLogic
{
	public class SearchByNameOrOneIngredient : ISearchByNameOrOneIngredient
	{
        private readonly IDrinkRepository _drinkRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly ISearchByIngredients _searchByIngredients;
        private readonly IMapper _mapper;

        public SearchByNameOrOneIngredient(IDrinkRepository drinkRepository, IIngredientRepository ingredientRepository, ISearchByIngredients searchByIngredients, IMapper mapper)
        {
            _drinkRepository = drinkRepository;
            _ingredientRepository = ingredientRepository;
            _searchByIngredients = searchByIngredients;
            _mapper = mapper;
        }

        private async Task<List<string>> GetAllDrinkNamesDistinct()
        {
            var allDrinksNames = await _drinkRepository.GetAll()
                .Select(i => i.Name)
                .Distinct()
                .ToListAsync();

            return allDrinksNames;
        }

        public async Task<List<DrinkDto>> SearchByName(string input)
        {
            var drinks = new List<DrinkDto>();
            if(input == null || input.Length < 3)
            {
                return drinks;
            }

            var allDrinksNames = await GetAllDrinkNamesDistinct();

            string pattern = $"([^a-z]|^){input}([A-Z]|[a-z])*";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);

            var matchingNames = allDrinksNames
                .Where(s => regex.IsMatch(s))
                .ToList();


            foreach(var name in matchingNames)
            {
                var drink = _mapper.Map<DrinkDto>(await _drinkRepository.SearchByNameAsync(name));
                drinks.Add(drink);
            }

            var drinksFromIngredientSearch = await SearchByOneIngredient(input);

            drinks.AddRange(drinksFromIngredientSearch);

            var drinksToShow = new List<DrinkDto>();

            foreach(var drink in drinks)
            {
                if (drinksToShow.Where(d => d.DrinkId == drink.DrinkId).Count() != 0)
                    continue;

                drinksToShow.Add(drink);
            }

            return drinksToShow.ToList();
        }

        

        public async Task<List<DrinkDto>> SearchByOneIngredient(string input)
        {
            var drinksDto = new List<DrinkDto>();

            if(input.Length < 3)
            {
                return drinksDto;
            }

            var ingredientMatch = await _searchByIngredients.GetAllIngredientsMatchingNames(input);
            if(ingredientMatch == null )
            {
                return drinksDto;
            }

            var ingredientName = ingredientMatch.Take(3).ToList();
            var ingredients = new List<Ingredient>();
            foreach (var ing in ingredientName)
            {
                var ingredientsToAdd = await _ingredientRepository.SearchByNameQueryable(ing).ToListAsync();
                ingredients.AddRange(ingredientsToAdd);
            }

            if(ingredients == null) 
            { 
                return drinksDto; 
            }

            foreach(var ingredient in ingredients)
            {
                var drinks = await _drinkRepository.GetDrinksByIngredientId(ingredient.IngredientId);

                if(drinks == null)
                {
                    continue;
                }

                foreach(var drink in drinks)
                {
                    var drinkDto = _mapper.Map<DrinkDto>(drink);
                    drinksDto.Add(drinkDto);
                }
            }

            return drinksDto;
        }
    }
}
