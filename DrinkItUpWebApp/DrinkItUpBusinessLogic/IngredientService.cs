﻿using AutoMapper;
using DrinkItUpBusinessLogic.DTOs;
using DrinkItUpBusinessLogic.Interfaces;
using DrinkItUpWebApp.DAL.Entities;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DrinkItUpBusinessLogic
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _repository;
        private readonly IDrinkIngredientRepository _drinkIngredientRepository;
        private readonly IMapper _mapper;

        public IngredientService(IIngredientRepository ingredientRepository, IDrinkIngredientRepository drinkIngredientRepository, IMapper mapper)
        {
            _repository = ingredientRepository;
            _drinkIngredientRepository = drinkIngredientRepository;
            _mapper = mapper;
        }
        public async Task<List<IngredientDto>> GetAllIngredientsWithUnits()
        {
            var ingredientsDto = new List<IngredientDto>();

            var ingredientsEntities = await _repository.GetAll()
                                        .Include(i => i.Unit)
                                        .ToListAsync();

            var ingredientsUsedInDrinksIds = await _drinkIngredientRepository.GetAll()
                        .Select(i => i.IngredientId).ToListAsync();

            foreach (var ingredient in ingredientsEntities)
            {
                var ingredientDto = _mapper.Map<IngredientDto>(ingredient);

                ingredientDto.IsUsed = ingredientsUsedInDrinksIds
                        .Contains(ingredientDto.IngredientId);

                ingredientsDto.Add(ingredientDto);
            }

            return ingredientsDto.OrderBy(i => i.Name).ToList();
        }

        public async Task<IngredientDto> GetById(int id)
        {
            var ingredient = _mapper.Map<IngredientDto>(await _repository.GetById(id) ?? new Ingredient());
            if (ingredient.Name != null)
            {
            ingredient.IsUsed = await IngredientIsUsed(id);
            }
            return ingredient;
        }

        public async Task<IngredientDto> Add(IngredientDto ingredientDto)
        {
            if (string.IsNullOrWhiteSpace(ingredientDto.Name)|| ingredientDto.UnitId == 0)
            {
                throw new Exception("Trying to add Ingredient without Name or UnitId");
            }
            var ingredient = _mapper.Map<Ingredient>(ingredientDto);
            ingredient = await _repository.Add(ingredient);
            await _repository.Save();

            ingredientDto = _mapper.Map<IngredientDto>(ingredient);

            return ingredientDto;
        }

        public async Task<bool> IngredientIsUsed(int id)
        {
            return await _drinkIngredientRepository.GetAll().Select(i => i.IngredientId).ContainsAsync(id);
        }

        public async Task<bool> IsIngredientUnique(IngredientDto ingredientToCheck)
        {
            if (string.IsNullOrWhiteSpace(ingredientToCheck.Name) || ingredientToCheck.UnitId == 0)
            {
                throw new Exception("Trying to check ingredient without name or UnitId");
            }
            var ingredients = await _repository.GetAll()
                .Where(i => i.Name== ingredientToCheck.Name && i.UnitId == ingredientToCheck.UnitId)
                .ToListAsync();   

            return !ingredients.Any();

        }

        public async Task<IngredientDto> Update(IngredientDto ingredient)
        {
            if (string.IsNullOrWhiteSpace(ingredient.Name) || ingredient.UnitId == 0)
            {
                throw new Exception("Trying to update ingredient without name or UnitId");
            }
            var ingredientToUpdate = _mapper.Map<Ingredient>(ingredient);
            _repository.Update(ingredientToUpdate);
            await _repository.Save();

            return ingredient;

        }

        public async Task<bool> Remove(int id)
        {
            var ingredient = await _repository.GetById(id);
            if(ingredient != null)
            {
                _repository.Delete(ingredient);
                await _repository.Save();
                return true;
            }

            return false;
            
        }

        public async Task<IEnumerable<UnitDto>> GetAllUnitsForIngredient(string ingredient)
        {
            var ingredients = await GetAllIngredientsWithUnits();

            return ingredients.Where(i => i.Name == ingredient).Select(i => i.Unit);    

        }

        public async Task<IngredientDto> GetByNameAndUnit(string ingredient, int unitId)
        {
            return _mapper.Map<IngredientDto>(await _repository.GetAll().
                FirstOrDefaultAsync(i => i.Name == ingredient && i.UnitId == unitId) ?? throw new Exception("Ingredient not found"));
        }
    }
}
