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
    public class IngredientRepository : CRDRepository<Ingredient>, IIngredientUpdateRepository, ISearchByNameQueryable<Ingredient>
    {
        private DrinkContext _context;

        public IngredientRepository(DrinkContext drinkContext) : base(drinkContext) 
        {
            _context= drinkContext;
        }

        public IQueryable<Ingredient> SearchByNameQueryable(string name)
        {
            var ingredients = _context.Ingredients
                .Include(i => i.Unit)
                .Where(i => i.Name == name)
                .AsQueryable();

            return ingredients;
        }

        public async Task<Ingredient?> Update(Ingredient ingredient)
        {
            var updatedIngredient = await GetById(ingredient.IngredientId);
            if(updatedIngredient != null)
            {
                updatedIngredient.Name= ingredient.Name;
                updatedIngredient.UnitId= ingredient.UnitId;
                await Save();
            }
            return updatedIngredient;
        }
    }
}
