using AutoMapper;
using DrinkItUpBusinessLogic.DTOs;
using DrinkItUpBusinessLogic.Interfaces;
using DrinkItUpWebApp.DAL.Entities;
using DrinkItUpWebApp.DAL.Repositories;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DrinkItUpBusinessLogic
{

    public class DifficultyService : IDifficultyService
    {
        private readonly IDifficultyRepository _repository;
        private readonly IMapper _mapper;
        private readonly IDrinkRepository _drinkRepository;

        public DifficultyService(IDifficultyRepository repository, IMapper mapper, IDrinkRepository drinkRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _drinkRepository = drinkRepository;
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

        public async Task<DifficultyDto> GetByName(string name)
        {
            var difficulty = await _repository.SearchByNameQueryable(name).FirstOrDefaultAsync();
            var difficultyDto = _mapper.Map<DifficultyDto>(difficulty);
            return difficultyDto;
        }

        public async Task<bool> IsDifficultyUsed(int id)
        {
            return await _drinkRepository.GetAll().Select(i => i.DifficultyId).ContainsAsync(id);
        }

        public async Task<bool> IsDifficultyUnique(string name)
        {
            var isUnique = !await _repository.GetAll().Where(u => u.Name == name).AnyAsync();
            return isUnique;
        }

        public async Task<bool> Remove(int id)
        {
            var difficulty = await _repository.GetById(id);
            if (difficulty == null)
                return false;

            _repository.Delete(difficulty);
            await _repository.Save();

            return true;
        }

        public async Task<DifficultyDto> Update(DifficultyDto difficultyDto)
        {
            var difficulty = _mapper.Map<Difficulty>(difficultyDto);
            _repository.Update(difficulty);
            await _repository.Save();

            return difficultyDto;
        }
    }

}

