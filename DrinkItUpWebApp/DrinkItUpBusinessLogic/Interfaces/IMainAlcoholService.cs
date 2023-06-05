﻿using DrinkItUpBusinessLogic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpBusinessLogic.Interfaces
{
    public interface IMainAlcoholService
    {
        Task<MainAlcoholDto> AddMainAlcohol(MainAlcoholDto mainAlcohol);
        Task<List<MainAlcoholDto>> GetAll();
    }
}
