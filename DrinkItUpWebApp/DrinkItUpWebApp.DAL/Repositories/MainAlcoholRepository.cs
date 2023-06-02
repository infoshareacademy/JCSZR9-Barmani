using DrinkItUpWebApp.DAL.Entities;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpWebApp.DAL.Repositories
{
    public class MainAlcoholRepository : CRUDRepository<MainAlcohol>, IMainAlcoholRepository
    {
        private readonly DrinkContext _context;

        public MainAlcoholRepository(DrinkContext context) : base(context)
        {
            _context = context;
        }
    }
}
