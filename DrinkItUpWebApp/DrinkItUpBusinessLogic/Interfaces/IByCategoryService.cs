using DrinkItUpBusinessLogic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpBusinessLogic.Interfaces
{
	public interface IByCategoryService
	{
		Task<IEnumerable<DrinkDto>> GetDrinksByMainAlcoholId(int mainAlcoholId);

		Task<IEnumerable<DrinkDto>> GetDrinksByDifficultyId(int difficultyId);
	}
}
