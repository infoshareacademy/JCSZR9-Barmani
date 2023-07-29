using DrinkItUpWebApp.DAL.Entities;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpWebApp.DAL.Repositories
{
    public class RoleRepository : CRUDRepository<Role>, IRoleRepository
    {
        private readonly DrinkContext _context;

        public RoleRepository(DrinkContext context) : base(context)
        {
            _context = context;
        }
    }
}
