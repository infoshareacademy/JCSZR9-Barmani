using DrinkItUpWebApp.DAL.Entities;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;


namespace DrinkItUpWebApp.DAL.Repositories
{
    public class UserRepository : CRUDRepository<User>, IUserRepository
    {
        private readonly DrinkContext _context;

        public UserRepository(DrinkContext context) : base(context) 
        {
            _context= context;
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users.FindAsync(email);
        }
    }
}
