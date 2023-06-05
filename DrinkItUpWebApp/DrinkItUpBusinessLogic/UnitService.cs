using AutoMapper;
using DrinkItUpBusinessLogic.DTOs;
using DrinkItUpBusinessLogic.Interfaces;
using DrinkItUpWebApp.DAL.Entities;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpBusinessLogic
{
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository _repository;
        private readonly IMapper _mapper;

        public UnitService(IUnitRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UnitDto> AddUnit(UnitDto unitDto)
        {

            var unitEntity = _mapper.Map<Unit>(unitDto);
            await _repository.Add(unitEntity);
            await _repository.Save();

            return unitDto;
            
        }

        public async Task<UnitDto> GetById(int id)
        {
            var unit = _mapper.Map<UnitDto>( await _repository.GetById(id));

            return unit;
        }

        public async Task<List<UnitDto>> GetAll()
        {
            var units = await _repository.GetAll().ToListAsync();
            var unitsDto = new List<UnitDto>();
            foreach(var unit in units) 
            {
                var unitDto = _mapper.Map<UnitDto>(unit);
                unitsDto.Add(unitDto);
            }
            return unitsDto;
        }
    }
}
