using DrinkItUpWebApp.DAL.Entities;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpWebApp.DAL.Repositories
{
    public class IngredientRepository : CRDRepository<Ingredient>, IIngredientUpdateRepository
    {
        private DrinkContext _context;

        public IngredientRepository(DrinkContext drinkContext) : base(drinkContext) 
        {
            _context= drinkContext;
        }
        public Ingredient? Update(Ingredient ingredient)
        {
            var updatedIngredient = GetById(ingredient.IngredientId);
            if(updatedIngredient != null)
            {
                updatedIngredient.Name= ingredient.Name;
                updatedIngredient.UnitId= ingredient.UnitId;
                Save();
            }
            return updatedIngredient;
        }
    }
}
