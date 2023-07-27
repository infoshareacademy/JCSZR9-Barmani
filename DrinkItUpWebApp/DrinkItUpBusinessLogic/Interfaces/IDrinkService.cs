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

        bool VerifyDrink(DrinkWithDetailsDto drink);

        Task<bool> IsDrinkUnique(DrinkWithDetailsDto drink);

        Task<DrinkDto> AddDrink(DrinkWithDetailsDto drink);

        Task<DrinkDto> UpdateDrink(DrinkWithDetailsDto drink);

        Task<bool> RemoveDrink(int id);


    }
}
