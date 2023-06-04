using DrinkItUpWebApp.DAL.Entities;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<Ingredient>> GetIngredientsByDrinkId(int id)
        {
            var drinkIngredients = await _drinkIngredientRepository.GetIngredientsByDrinkId(id).ToListAsync();


            var results = new List<Ingredient>();    

            foreach(var drinkIngredient in drinkIngredients)
            {
                var ingredient = await _context.Ingredients
                    .Include(i => i.DrinkIngredients)
                    .Include(i => i.Unit)
                    .FirstOrDefaultAsync(i => i.IngredientId == drinkIngredient.IngredientId);

                results.Add(ingredient);
            }

            return results;
        }
    }
}
