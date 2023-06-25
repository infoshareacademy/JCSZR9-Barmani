using AutoMapper;
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
            var ingredient = _mapper.Map<IngredientDto>(await _repository.GetById(id));
            ingredient.IsUsed = await IngredientIsUsed(id);
            return ingredient;
        }

        public async Task<IngredientDto> Add(IngredientDto ingredientDto)
        {
            var ingredient = _mapper.Map<Ingredient>(ingredientDto);
            await _repository.Add(ingredient);
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
            var ingredients = await _repository.GetAll()
                .Where(i => i.Name== ingredientToCheck.Name && i.UnitId == ingredientToCheck.UnitId)
                .ToListAsync();   

            return !ingredients.Any();

        }

        public async Task<IngredientDto> Update(IngredientDto ingredient)
        {
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
    }
}
