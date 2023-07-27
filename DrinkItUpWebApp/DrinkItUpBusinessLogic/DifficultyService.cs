using AutoMapper;
using DrinkItUpBusinessLogic.DTOs;
using DrinkItUpBusinessLogic.Interfaces;
using DrinkItUpWebApp.DAL.Entities;
using DrinkItUpWebApp.DAL.Repositories;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DrinkItUpBusinessLogic
{

    public class DifficultyService : IDifficultyService
    {
        private readonly IDifficultyRepository _repository;
        private readonly IMapper _mapper;
        private readonly IDrinkRepository _drinkRepository;
        private readonly ILogger<DifficultyService> _logger;

        public DifficultyService(IDifficultyRepository repository, IMapper mapper, IDrinkRepository drinkRepository, ILogger<DifficultyService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _drinkRepository = drinkRepository;
            _logger = logger;
        }

        public async Task<DifficultyDto> AddDifficulty(DifficultyDto difficultyDto)
        {
            if (string.IsNullOrWhiteSpace(difficultyDto.Name))
                throw new Exception("Trying to add difficulty without a name");

            var difficultyEntity = _mapper.Map<Difficulty>(difficultyDto);
            await _repository.Add(difficultyEntity);
            await _repository.Save();

            _logger.LogInformation($"{DateTime.Now}: New Difficulty {difficultyEntity.Name} has been added.");
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
			return _mapper.Map<DifficultyDto>(await _repository.GetById(id) ?? new Difficulty());
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
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Trying to check difficulty without a name");

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

            _logger.LogInformation($"{DateTime.Now}: Difficulty {difficulty.Name} has been removed.");
            return true;
        }

        public async Task<DifficultyDto> Update(DifficultyDto difficultyDto)
        {
            if (string.IsNullOrWhiteSpace(difficultyDto.Name))
                throw new Exception("Trying to update difficulty without a name");

            var difficulty = _mapper.Map<Difficulty>(difficultyDto);
            _repository.Update(difficulty);
            await _repository.Save();

            _logger.LogInformation($"{DateTime.Now}: Difficulty {difficulty.Name} has been updated.");
            return difficultyDto;
        }
    }
}

