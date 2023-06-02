﻿using DrinkItUpWebApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpBusinessLogic.DTOs
{
	public class DrinkDto
	{
		public int DrinkId { get; set; }
		public string Name { get; set; }

		public int MainAlcoholId { get; set; }

		public int DifficultyId { get; set; }

		public string Description { get; set; } 
	}
}
