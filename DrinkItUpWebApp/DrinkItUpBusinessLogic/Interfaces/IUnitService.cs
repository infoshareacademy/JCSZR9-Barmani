using DrinkItUpBusinessLogic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpBusinessLogic.Interfaces
{
    public interface IUnitService
    {
        Task<UnitDto> GetById(int id);
        Task<List<UnitDto>> GetAll();
        Task<UnitDto> AddUnit(UnitDto unit); 

        Task<bool> UnitIsUsed(int id);
    }
}
