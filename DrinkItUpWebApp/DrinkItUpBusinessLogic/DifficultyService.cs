using AutoMapper;
using DrinkItUpBusinessLogic.DTOs;
using DrinkItUpBusinessLogic.Interfaces;
using DrinkItUpWebApp.DAL.Entities;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DrinkItUpBusinessLogic
{

    public class DifficultyService : IDifficultyService
    {
        private readonly IDifficultyRepository _repository;
        private readonly IMapper _mapper;

        public DifficultyService(IDifficultyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DifficultyDto> AddDifficulty(DifficultyDto difficultyDto)
        {

            var difficultyEntity = _mapper.Map<Difficulty>(difficultyDto);
            await _repository.Add(difficultyEntity);
            await _repository.Save();

            return difficultyDto;

        }

        public async Task<List<DifficultyDto>> GetAll()
        {
            var difficulties = await _repository.GetAll().ToListAsync();
            var difficultiesDto = new List<DifficultyDto>();
            
            foreach (var difficulty in difficulties)
            {
                var difficultyDto = _mapper.Map<DifficultyDto>(difficulty);
                difficultiesDto.Add(difficultyDto);
            }
            return difficultiesDto;
        }

		public async Task<DifficultyDto> GetById(int id)
		{
			return _mapper.Map<DifficultyDto>(await _repository.GetById(id));
		}
	}

}

