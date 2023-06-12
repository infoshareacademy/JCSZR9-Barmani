using DrinkItUpWebApp.DAL.Entities;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DrinkItUpWebApp.DAL.Repositories

{
    public class DrinkRepository : CRUDRepository<Drink>, IDrinkRepository
    {
        private DrinkContext _context;
        private readonly IDrinkIngredientRepository _drinkIngredientRepository;

        public DrinkRepository(DrinkContext drinkContext, IDrinkIngredientRepository drinkIngredientRepository) : base(drinkContext)
        {
            _context = drinkContext;
            _drinkIngredientRepository = drinkIngredientRepository;
        }

        public async Task<Drink> GetByIdWithDetails(int id)
        {
            var drink = await _context.Drinks
                .Include(d => d.MainAlcohol)
                .Include(d => d.Difficulty)
                .FirstOrDefaultAsync(d => d.DrinkId == id);

            return drink;
        }

        public async Task<List<int>> GetDrinksIdByIngredientId(int id)
		{
            var drinksId = await _context.Drinks
                .Include(d => d.MainAlcohol)
                .Include(d => d.Difficulty)
                .Include(d => d.DrinkIngredients)
                .SelectMany(d => d.DrinkIngredients)
                .Where(d => d.IngredientId == id)
                .Select(d => d.DrinkId)
                .ToListAsync();

            return drinksId;
		}

		public async Task<Drink> SearchByNameAsync(string name)
        {
            var drink = await _context.Drinks
                .Include(d => d.MainAlcohol)
                .Include(d => d.Difficulty)
                .Include(d => d.DrinkIngredients)
                .ThenInclude(d => d.Ingredient)
                .FirstOrDefaultAsync(d => d.Name == name);


            return drink;
        }

        public IQueryable<Drink> SearchByNameQueryable(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Drink>> GetDrinksByIngredientId(int id)
        {
            var drinkIngredients = await _drinkIngredientRepository.GetDrinksByIngredientId(id).ToListAsync();

            var results = new List<Drink>();

            foreach (var drinkIngredient in drinkIngredients)
            {
                var drink = await _context.Drinks
                    .Include(i => i.DrinkIngredients)
                    .FirstOrDefaultAsync(i => i.DrinkId == drinkIngredient.DrinkId);

                results.Add(drink);
            }

            return results;
        }
    }
}
