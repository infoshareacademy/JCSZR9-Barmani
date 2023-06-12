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
		Task<DrinkDto> GetDrinksByMainAlcoholId(int mainAlcoholId);

		Task<DrinkDto> GetDrinksByDifficultyId(int difficultyId);
	}
}
