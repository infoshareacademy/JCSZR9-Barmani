using AutoMapper;
using DrinkItUpBusinessLogic.DTOs;
using DrinkItUpBusinessLogic.Interfaces;
using DrinkItUpWebApp.DAL.Entities;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
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
			var matchingNames = new List<string>();
			if(input == string.Empty || input == null)
			{
				return matchingNames;
			}
			var allIngredientsNames = await GetAllNamesDistinct();

			string pattern = $"([^a-z]|^){input}([A-Z]|[a-z])*";
			var regex = new Regex(pattern, RegexOptions.IgnoreCase);

			matchingNames = allIngredientsNames
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

		public async Task<Dictionary<string,List<DrinkDto>>> GetMatchingDrinksToIngredients(string input)
		{
			var matchingIngredientsToDrinks = new Dictionary<string, List<int>>();

			var ingredients = GetAllNamesFromSumbit(input);

			foreach(var ingredient in ingredients)
			{
				matchingIngredientsToDrinks.Add(ingredient, new List<int>());

				var ingredientsEntities = await _ingredientRepository.SearchByNameQueryable(ingredient).ToListAsync();

				foreach(var entities in ingredientsEntities)
				{
					var drinksId = await _drinkRepository.GetDrinksIdByIngredientId(entities.IngredientId);
					matchingIngredientsToDrinks[ingredient].AddRange(drinksId);
				}
			}

			var results = matchingIngredientsToDrinks
				.SelectMany(l => l.Value)
				.GroupBy(l => l)
				.OrderByDescending(l => l.Count())
				.ToList();
				

				


			var drinks = new Dictionary<string, List<DrinkDto>>();


			foreach (var key in results)
			{

				string msg = $"Pasujące składniki: {key.Count()}";
				var drink = _mapper.Map<DrinkDto>(await _drinkRepository.GetByIdWithDetails(key.Key));
				if (drinks.ContainsKey(msg))
					drinks[msg].Add(drink);
				else
					drinks.Add(msg, new List<DrinkDto> { drink });

			}



			return drinks;
		}
	}
}