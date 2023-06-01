using DrinkItUpWebApp.DAL.Entities;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpWebApp.DAL.Repositories
{
	public class DrinkIngredientRepository : CRUDRepository<DrinkIngredient>, IDrinkIngedientRepository
	{
		private readonly DrinkContext _context;

		public DrinkIngredientRepository(DrinkContext drinkContext) : base(drinkContext)
		{
			_context = drinkContext;
		}

		public Drink GetDrinkByIngredientId(int id)
		{
			throw new NotImplementedException();
		}
	}
}
