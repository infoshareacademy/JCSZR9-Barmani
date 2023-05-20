using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpWebApp.DAL.Repositories.Interfaces
{
    public interface ICRDRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        T? GetById(int id);

        T Add(T entity);

        T Delete(T entity);

        void Save();


    }
}
