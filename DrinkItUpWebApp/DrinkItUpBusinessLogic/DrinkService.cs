﻿using AutoMapper;
using DrinkItUpBusinessLogic.DTOs;
using DrinkItUpBusinessLogic.Interfaces;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DrinkItUpBusinessLogic
{
    public class DrinkService : IDrinkService
    {
        private readonly IDrinkRepository _repository;
        private readonly IMapper _mapper;

        public DrinkService(IDrinkRepository drinkRepository, IMapper mapper)
        {
            _repository= drinkRepository;
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
	}
}