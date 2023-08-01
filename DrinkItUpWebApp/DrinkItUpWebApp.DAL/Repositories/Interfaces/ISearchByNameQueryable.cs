using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpWebApp.DAL.Repositories.Interfaces
{
    public interface ISearchByNameQueryable<T> where T : class
    {
        IQueryable<T> SearchByNameQueryable(string name);

    }
}
