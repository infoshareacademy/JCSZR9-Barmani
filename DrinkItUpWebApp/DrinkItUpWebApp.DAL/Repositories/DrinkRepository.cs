using DrinkItUpWebApp.DAL.Entities;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DrinkItUpWebApp.DAL.Repositories

{
    public class DrinkRepository : CRUDRepository<Drink>, IDrinkRepository
    {
        private DrinkContext _context;

        public DrinkRepository(DrinkContext drinkContext) : base(drinkContext)
        {
            _context = drinkContext;
        }

        public async Task<Drink> GetByIdWithDetails(int id)
        {
            var drink = await _context.Drinks
                .Include(d => d.MainAlcohol)
                .Include(d => d.Difficulty)
                .FirstOrDefaultAsync(d => d.DrinkId == id);

            return drink;
        }

        public  List<int> GetDrinksIdByIngredientId(int id)
		{
            var drinksId = _context.Drinks
                .Include(d => d.DrinkIngredients)
                .SelectMany(d => d.DrinkIngredients)
                .Where(d => d.IngredientId == id)
                .Select(d => d.DrinkId)
                .ToList();

            return drinksId;
		}

		public IQueryable<Drink> SearchByNameQueryable(string name)
        {
            var drinkss = _context.Drinks
                .Include(d => d.MainAlcohol)
                .Include(d => d.Difficulty)
                .Include(d => d.DrinkIngredients)
                .ThenInclude(d => d.Ingredient)
                .Where(d => d.Name == name)
                .AsQueryable();

            return drinkss;
        }

        
    }
}
