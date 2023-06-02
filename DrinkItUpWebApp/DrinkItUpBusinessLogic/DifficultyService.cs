using AutoMapper;
using DrinkItUpBusinessLogic.DTOs;
using DrinkItUpBusinessLogic.Interfaces;
using DrinkItUpWebApp.DAL.Entities;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;

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

    }

}

