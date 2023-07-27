﻿using AutoMapper;
using DrinkItUpBusinessLogic.DTOs;
using DrinkItUpBusinessLogic.Interfaces;
using DrinkItUpWebApp.DAL.Entities;
using DrinkItUpWebApp.DAL.Repositories;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DrinkItUpBusinessLogic
{
    public class DrinkService : IDrinkService
    {
        private readonly IDrinkRepository _repository;
        private readonly IDrinkIngredientRepository _drinkIngredientRepository;
        private readonly IMapper _mapper;

        public DrinkService(IDrinkRepository drinkRepository, IDrinkIngredientRepository drinkIngredientRepository, IMapper mapper)
        {
            _repository= drinkRepository;
            _drinkIngredientRepository = drinkIngredientRepository;
            _mapper = mapper;
        }
        public async Task<DrinkDto> GetByIdWithCategories(int id)
        {
            var drink = await _repository.GetAll()
                            .Include(d => d.MainAlcohol)
                            .Include(d => d.Difficulty)
                            .FirstOrDefaultAsync(d => d.DrinkId== id);

            var drinkDto = _mapper.Map<DrinkDto>(drink);

            return drinkDto;
        }

		public async Task<IEnumerable<DrinkDto>> GetAll()
		{
            var drinks = await _repository.GetAll()
                            .Include(d => d.MainAlcohol)
                            .Include(d => d.Difficulty)
                            .ToListAsync();


            var drinksDto = drinks.Select(d => _mapper.Map<DrinkDto>(d));

			return drinksDto;
		}

        public bool VerifyDrink(DrinkWithDetailsDto drink)
        {
            if(string.IsNullOrWhiteSpace(drink.Name) ||
                drink.Ingredients.Count() < 2 ||
                drink.MainAlcohol.MainAlcoholId == 0 ||
                drink.Difficulty.DifficultyId == 0 ||
                drink.Description.Length < 10 
                )
                return false;

            return true;

        }

        public async Task<bool> IsDrinkUnique(DrinkWithDetailsDto drink)
        {
            return !await _repository.GetAll()
                .AnyAsync(d => d.Name == drink.Name && 
                d.MainAlcoholId == drink.MainAlcohol.MainAlcoholId &&
                d.DifficultyId == drink.Difficulty.DifficultyId);
        }

        public async Task<DrinkDto> AddDrink(DrinkWithDetailsDto drink)
        {
            if (!VerifyDrink(drink))
                throw new Exception("Lack of Data in Drink");

            foreach(var ingredient in drink.Ingredients)
            {
                var drinkIngredient = new DrinkIngredient { DrinkId = drink.DrinkId, Quantity = ingredient.Quantity, IngredientId = ingredient.IngredientId };
                await _drinkIngredientRepository.Add(drinkIngredient);
            }

            var drinkEntity = await _repository.Add(_mapper.Map<Drink>(drink));
            await _repository.Save();

            var drinkDto = _mapper.Map<DrinkDto>(drinkEntity);

            return drinkDto;
        }

        public async Task<DrinkDto> UpdateDrink(DrinkWithDetailsDto drink)
        {
            if (!VerifyDrink(drink))
                throw new Exception("Lack of Data in Drink");

            await RemoveDrink(drink.DrinkId);
            var updateddrink = await AddDrink(drink);

            return updateddrink;

        }

        public async Task<bool> RemoveDrink(int id)
        {
            var drink = await _repository.GetById(id);
            if (drink == null)
                return false;

            var drinkIngredients = await _drinkIngredientRepository.GetAll()
                .Where(dI => dI.DrinkId == id)
                .ToListAsync();

            drinkIngredients.ForEach(d => _drinkIngredientRepository.Delete(d));

            _repository.Delete(drink);
            await _repository.Save();

            return true;
        }
    }
}
