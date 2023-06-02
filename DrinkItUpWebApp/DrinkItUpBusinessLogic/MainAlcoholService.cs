using AutoMapper;
using DrinkItUpBusinessLogic.DTOs;
using DrinkItUpBusinessLogic.Interfaces;
using DrinkItUpWebApp.DAL.Entities;
using DrinkItUpWebApp.DAL.Repositories;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;
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

    }
}
