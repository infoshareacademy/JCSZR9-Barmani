using AutoMapper;
using DrinkItUpBusinessLogic.DTOs;
using DrinkItUpBusinessLogic.Interfaces;
using DrinkItUpWebApp.DAL.Entities;
using DrinkItUpWebApp.DAL.Repositories;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpBusinessLogic
{
    public class MainAlcoholService : IMainAlcoholService
    {
        private readonly IMainAlcoholRepository _repository;
        private readonly IMapper _mapper;

        public MainAlcoholService(IMainAlcoholRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<MainAlcoholDto> AddMainAlcohol(MainAlcoholDto mainAlcoholDto)
        {

            var mainAlcoholEntity = _mapper.Map<MainAlcohol>(mainAlcoholDto);
            await _repository.Add(mainAlcoholEntity);
            await _repository.Save();

            return mainAlcoholDto;

        }

        public async Task<List<MainAlcoholDto>> GetAll()
        {
            var mainAlcohols = await _repository.GetAll().ToListAsync();
            var mainAlcoholsDto = new List<MainAlcoholDto>();

            foreach(var mainAlcohol in mainAlcohols)
            {
                var mainAlcoholDto = _mapper.Map<MainAlcoholDto>(mainAlcohol);
                mainAlcoholsDto.Add(mainAlcoholDto);
            }
            return mainAlcoholsDto;
        }

        public async Task<MainAlcoholDto> GetById(int id)
        {
            return _mapper.Map<MainAlcoholDto>(await _repository.GetById(id)); 
        }

        public async Task<MainAlcoholDto> GetByName(string name)
        {
            var mainAlcohol = await _repository.SearchByNameQueryable(name).FirstOrDefaultAsync();
            var mainAlcoholDto = _mapper.Map<MainAlcoholDto>(mainAlcohol);
            return mainAlcoholDto;
        }
    }
}
