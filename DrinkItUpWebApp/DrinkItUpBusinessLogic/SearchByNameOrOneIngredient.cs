using AutoMapper;
using DrinkItUpBusinessLogic.DTOs;
using DrinkItUpBusinessLogic.Interfaces;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DrinkItUpBusinessLogic
{
	public class SearchByNameOrOneIngredient : ISearchByNameOrOneIngredient
	{
        private readonly IDrinkRepository _drinkRepository;
        private readonly IMapper _mapper;

        public SearchByNameOrOneIngredient(IDrinkRepository drinkRepository, IMapper mapper)
        {
            _drinkRepository = drinkRepository;
            _mapper = mapper;
        }

        public List<string> GetAllNamesDistinct()
        {
            var allDrinksNames = _drinkRepository.GetAll()
                .Select(i => i.Name)
                .Distinct()
                .ToList();

            return allDrinksNames;
        }

        public async Task<List<DrinkDto>> SearchByName(string input)
        {
            var allDrinksNames = GetAllNamesDistinct();

            string pattern = $"([^a-z]|^){input}([A-Z]|[a-z])*";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);

            var matchingNames = allDrinksNames
                .Where(s => regex.IsMatch(s))
                .Take(3)
                .ToList();

            var drinks = new List<DrinkDto>();

            foreach(var name in matchingNames)
            {
                var drink = _mapper.Map<DrinkDto>(await _drinkRepository.SearchByNameAsync(name));
                drinks.Add(drink);
            }

            return drinks;
        }

        

        public void SearchByOneIngredient(string input)
        {
            throw new NotImplementedException();
        }
    }
}
