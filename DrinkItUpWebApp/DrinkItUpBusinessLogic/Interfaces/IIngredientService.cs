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

        Task<IEnumerable<UnitDto>> GetAllUnitsForIngredient(string ingredient);
        Task<IngredientDto> GetByNameAndUnit(string ingredient, int unitId);

        Task<IngredientDto> GetById(int id);

        Task<IngredientDto> Add(IngredientDto ingredient);

        Task<bool> IngredientIsUsed(int id);

        Task<bool> IsIngredientUnique(IngredientDto ingredientToCheck);

        Task<IngredientDto> Update(IngredientDto ingredient);

        Task<bool> Remove(int id);
    }
}
