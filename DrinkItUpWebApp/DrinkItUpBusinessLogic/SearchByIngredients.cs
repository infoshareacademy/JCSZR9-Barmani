﻿using AutoMapper;
using DrinkItUpWebApp.DAL.Repositories;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DrinkItUpBusinessLogic
{
	public class SearchByIngredients : ISearchByIngredients
	{
		private IIngredientRepository _ingredientRepository;
		private readonly IMapper _mapper;

		public SearchByIngredients(IIngredientRepository ingredientRepository, IMapper mapper)
		{
			_ingredientRepository = ingredientRepository;
			_mapper = mapper;
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