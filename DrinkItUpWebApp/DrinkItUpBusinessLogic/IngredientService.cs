using AutoMapper;
using DrinkItUpBusinessLogic.DTOs;
using DrinkItUpBusinessLogic.Interfaces;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace DrinkItUpBusinessLogic
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _repository;
        private readonly IMapper _mapper;

        public IngredientService(IIngredientRepository ingredientRepository, IMapper mapper)
        {
            _repository = ingredientRepository;
            _mapper = mapper;
        }
        public async Task<List<IngredientDto>> GetAllIngredientsWithUnits()
        {
            var ingredientsDto = new List<IngredientDto>();

            var ingredientsEntities = await _repository.GetAll()
                                        .Include(i => i.Unit)
                                        .ToListAsync();

            foreach(var ingredient in ingredientsEntities)
            {
                var ingredientDto = _mapper.Map<IngredientDto>(ingredient);

                ingredientDto.IsUsed = await _repository.GetAll()
                        .Include(i => i.DrinkIngredients)
                        .Select(i => i.IngredientId)
                        .ContainsAsync(ingredientDto.IngredientId);

                ingredientsDto.Add(ingredientDto);
            }

            return ingredientsDto.OrderBy(i => i.Name).ToList();
        }

        public async Task<IngredientDto> GetById(int id)
        {
            return _mapper.Map<IngredientDto>(await _repository.GetById(id));
        }
    }
}
