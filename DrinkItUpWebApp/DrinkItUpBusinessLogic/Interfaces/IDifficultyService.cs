using DrinkItUpBusinessLogic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpBusinessLogic.Interfaces
{
    public interface IDifficultyService
    {
        Task<DifficultyDto> AddDifficulty(DifficultyDto difficulty);

        Task<List<DifficultyDto>> GetAll();

        Task<DifficultyDto> GetById(int id);

        Task<DifficultyDto> GetByName(string name);

        Task<bool> IsDifficultyUsed(int id);

        Task<bool> IsDifficultyUnique(string name);

        Task<bool> Remove(int id);




    }
}
