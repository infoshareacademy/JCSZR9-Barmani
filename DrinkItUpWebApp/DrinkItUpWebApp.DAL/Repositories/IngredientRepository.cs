using DrinkItUpWebApp.DAL.Entities;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpWebApp.DAL.Repositories
{
    public class IngredientRepository : CRUDRepository<Ingredient>, IIngredientRepository
    {
        private DrinkContext _context;

        public IngredientRepository(DrinkContext drinkContext) : base(drinkContext) 
        {
            _context= drinkContext;
        }

        public async Task<List<Ingredient>> SearchByNameAsync(string name)
        {
            var ingredients = await _context.Ingredients
                .Include(i => i.Unit)
                .Where(i => i.Name == name)
                .ToListAsync();

            return ingredients;
        }
    
    }
}
