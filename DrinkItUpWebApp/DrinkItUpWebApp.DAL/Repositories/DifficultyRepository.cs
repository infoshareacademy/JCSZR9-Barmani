using DrinkItUpWebApp.DAL.Entities;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpWebApp.DAL.Repositories
{
    public class DifficultyRepository : CRUDRepository<Difficulty>, IDifficultyRepository
    {
        private readonly DrinkContext _context;

        public DifficultyRepository(DrinkContext context) : base(context)
        {
            _context = context;
        }
    }
}
