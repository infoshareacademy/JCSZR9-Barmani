using DrinkItUpWebApp.DAL.Entities;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpWebApp.DAL.Repositories.Interfaces
{
    public interface ICRUDRepository<T> where T : class
    {
        IQueryable<T> GetAll();

		T Update(T entity);

		Task<T?> GetById(int id);

        Task<T> Add(T entity);

        T Delete(T entity);

        Task Save();

    }
}
