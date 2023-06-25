using DrinkItUpWebApp.DAL.Entities;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpWebApp.DAL.Repositories
{
	public class DrinkIngredientRepository : CRUDRepository<DrinkIngredient>, IDrinkIngredientRepository
	{
		private readonly DrinkContext _context;

		public DrinkIngredientRepository(DrinkContext drinkContext) : base(drinkContext)
		{
			_context = drinkContext;
		}

        public IQueryable<DrinkIngredient> GetIngredientsByDrinkId(int id)
        {
            var drinkIngredients = _context.DrinkIngredients
                .Where(d => d.DrinkId == id);

            return drinkIngredients;
        }

        public IQueryable<DrinkIngredient> GetDrinksByIngredientId(int id)
        {
            var drinkIngredients = _context.DrinkIngredients
                .Where(d => d.IngredientId == id);

            return drinkIngredients;
        }
    }
}
