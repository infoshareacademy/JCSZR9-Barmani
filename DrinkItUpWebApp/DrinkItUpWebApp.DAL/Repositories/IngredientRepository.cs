using DrinkItUpWebApp.DAL.Entities;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpWebApp.DAL.Repositories
{
    public class IngredientRepository : CRUDRepository<Ingredient>, IIngredientRepository
    {
        private DrinkContext _context;
        private readonly IDrinkIngredientRepository _drinkIngredientRepository;

        public IngredientRepository(DrinkContext drinkContext, IDrinkIngredientRepository drinkIngredientRepository) : base(drinkContext) 
        {
            _context= drinkContext;
            _drinkIngredientRepository = drinkIngredientRepository;
        }

          

		public IQueryable<Ingredient> SearchByNameQueryable(string name)
		{
				var ingredients = _context.Ingredients
					.Include(i => i.Unit)
					.Where(i => i.Name == name)
					.AsQueryable();

				return ingredients;
			
		}

        public IQueryable<Ingredient> GetListOfIngredientsByDrinkId(int id)
        {
            var drinkIngredients = _drinkIngredientRepository.GetIngredientsByDrinkId(id)
                .

            return drinkIngredients;
        }
    }
}
