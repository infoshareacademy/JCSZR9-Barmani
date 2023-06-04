using AutoMapper;
using DrinkItUpBusinessLogic.DTOs;
using DrinkItUpBusinessLogic.Interfaces;
using DrinkItUpWebApp.DAL.Entities;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace DrinkItUpBusinessLogic
{
    public class SearchByIngredients : ISearchByIngredients
	{
		private IIngredientRepository _ingredientRepository;
		private IDrinkRepository _drinkRepository;
		private readonly IMapper _mapper;

		public SearchByIngredients(IIngredientRepository ingredientRepository,IDrinkRepository drinkRepository, IMapper mapper)
		{
			_ingredientRepository = ingredientRepository;
			_drinkRepository = drinkRepository;
			_mapper = mapper;
		}

		public async Task<List<string>> GetAllIngredientsMatchingNames(string input)
		{
			var allIngredientsNames = await GetAllNamesDistinct();

			string pattern = $"([^a-z]|^){input}([A-Z]|[a-z])*";
			var regex = new Regex(pattern, RegexOptions.IgnoreCase);

			var matchingNames = allIngredientsNames
				.Where(s => regex.IsMatch(s))
				.Take(5)
				.ToList();

			return matchingNames;
		}

		public async Task<List<string>> GetAllNamesDistinct()
		{
			var allIngredientsNames = await _ingredientRepository.GetAll()
				.Select(i => i.Name)
				.Distinct()
				.ToListAsync();

			return allIngredientsNames;
		}

		public List<string> GetAllNamesFromSumbit(string input)
		{
			var names = input.Split(',').ToList();
			return names;

		}

		public async Task<List<DrinkDto>> GetMatchingDrinksToIngredients(string input)
		{
			var matchingIngredientsToDrinks = new Dictionary<string, List<int>>();

			var ingredients = GetAllNamesFromSumbit(input);

			foreach(var ingredient in ingredients)
			{
				matchingIngredientsToDrinks.Add(ingredient, new List<int>());

				var ingredientsEntities = await _ingredientRepository.SearchByNameQueryable(ingredient).ToListAsync();

				foreach(var entities in ingredientsEntities)
				{
					var drinksId = _drinkRepository.GetDrinksIdByIngredientId(entities.IngredientId);
					matchingIngredientsToDrinks[ingredient].AddRange(drinksId);
				}
			}

			var results = matchingIngredientsToDrinks
				.SelectMany(l => matchingIngredientsToDrinks.Values)
				.SelectMany(d => d) // spłaszczamy słownik do listy id drinków samych
				.GroupBy(d => d)
				.OrderByDescending(k => k.Count())
				.Select(d => d.Key)
				.ToList();

			var drinks = new List<Drink>();

			foreach(var drinkId in results)
			{
				var drink = await _drinkRepository.GetById(drinkId);
				drinks.Add(drink);
			}

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