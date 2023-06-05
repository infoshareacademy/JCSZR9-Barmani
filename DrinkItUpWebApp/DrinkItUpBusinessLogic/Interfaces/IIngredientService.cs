using DrinkItUpBusinessLogic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpBusinessLogic.Interfaces
{
    public interface IIngredientService
    {
        Task<List<IngredientDto>> GetAllIngredientsWithUnits();

        Task<IngredientDto> GetById(int id);
    }
}
