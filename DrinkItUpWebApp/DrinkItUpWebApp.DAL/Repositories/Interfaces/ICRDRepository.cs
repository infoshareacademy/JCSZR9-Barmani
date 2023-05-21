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

        Task<T?> GetById(int id);

        Task<T> Add(T entity);

        Task<T> Delete(T entity);

        Task Save();


    }
}
