﻿using DrinkItUpBusinessLogic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpBusinessLogic.Interfaces
{
	public interface ISearchByNameOrOneIngredient
	{
		Task<List<DrinkDto>> SearchByName(string input);


		Task<List<DrinkDto>> SearchByOneIngredient(string input);

    }
}
