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

        public Task<bool> IngredientIsUsed(int id)
        {
            return _drinkIngredientRepository.GetAll().Select(i => i.IngredientId).ContainsAsync(id);
        }
    }
}
