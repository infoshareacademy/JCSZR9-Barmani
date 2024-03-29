﻿using DrinkItUpBusinessLogic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpBusinessLogic.Interfaces
{
    public interface IGetDrinkDetails
    {
        Task<DrinkWithDetailsDto> GetDrinkToDetailView(int id);
        Task<List<int>> GetAllDrinksId();
    }
}
