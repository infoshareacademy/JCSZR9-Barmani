using DrinkItUpBusinessLogic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpBusinessLogic.Interfaces
{
    public interface IDrinkService
    {
        Task<DrinkDto> GetByIdWithCategories(int id);

        Task<IEnumerable<DrinkDto>> GetAll();

	}
}
