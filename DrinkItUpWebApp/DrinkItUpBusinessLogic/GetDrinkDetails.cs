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
        private readonly IDrinkIngredientRepository _drinkIngredientRepository;
        private readonly IMapper _mapper;

        public GetDrinkDetails(IDrinkRepository drinkRepository, IDrinkIngredientRepository drinkIngredientRepository, IMapper mapper)
        {
            _drinkRepository = drinkRepository;
            _drinkIngredientRepository = drinkIngredientRepository;
            _mapper = mapper;
        }

        public async Task<DrinkWithDetailsDto> GetDrinkToDetailView(int id)
        {
            var drinkWithDeatailsDto = new DrinkWithDetailsDto();

            var drink = await _drinkRepository.GetById(id);

            var drinkIngredients = await _drinkIngredientRepository.GetIngredientsByDrinkId(id).ToListAsync();

            drinkWithDeatailsDto = _mapper.Map<DrinkWithDetailsDto>(drink);
            drinkWithDeatailsDto.Ingredients = drinkIngredientsDto;

            return drinkWithDeatailsDto;
        }
    }
}
