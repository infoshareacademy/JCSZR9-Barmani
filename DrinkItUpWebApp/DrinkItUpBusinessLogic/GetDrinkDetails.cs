using AutoMapper;
using DrinkItUpBusinessLogic.DTOs;
using DrinkItUpBusinessLogic.Interfaces;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpBusinessLogic
{
    public class GetDrinkDetails : IGetDrinkDetails
    {
        private readonly IDrinkRepository _drinkRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMapper _mapper;


        public GetDrinkDetails(IDrinkRepository drinkRepository, IIngredientRepository ingredientRepository, IMapper mapper)
        {
            _drinkRepository = drinkRepository;
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
        }

        public async Task<List<int>> GetAllDrinksId()
        {
            return await _drinkRepository.GetAll().Select(d => d.DrinkId).ToListAsync();
        }

        public async Task<DrinkWithDetailsDto> GetDrinkToDetailView(int id)
        {
            var drinkWithDeatailsDto = new DrinkWithDetailsDto();

            var drink = await _drinkRepository.GetByIdWithDetails(id);

            var drinkIngredients = await _ingredientRepository.GetIngredientsByDrinkId(id);

            drinkWithDeatailsDto = _mapper.Map<DrinkWithDetailsDto>(drink);

            foreach (var drinkIngredient in drinkIngredients)
            {
                var ingredient = _mapper.Map<IngredientDto>(drinkIngredient);
                var quantity = drinkIngredient.DrinkIngredients.FirstOrDefault(d => d.DrinkId == drinkWithDeatailsDto.DrinkId).Quantity;
                ingredient.Quantity = (decimal)quantity;
                drinkWithDeatailsDto.Ingredients.Add(ingredient);
            }

            return drinkWithDeatailsDto;
        }
    }
}
