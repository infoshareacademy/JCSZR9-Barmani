using DrinkItUpWebApp.DAL.Entities;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpWebApp.DAL.Repositories
{
    public class UnitRepository : CRUDRepository<Unit>, IUnitRepository
    {
        private readonly DrinkContext _context;

        public UnitRepository(DrinkContext context) : base(context) 
        {
            _context = context;
        }
    }
}
