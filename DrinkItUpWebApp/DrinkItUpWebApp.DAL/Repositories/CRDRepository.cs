using DrinkItUpWebApp.DAL.Entities;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpWebApp.DAL.Repositories
{
    public class CRDRepository<T> : ICRDRepository<T> where T : class
    {
        private DrinkContext _context;

        public CRDRepository(DrinkContext context)
        {
            _context = context;
        }

        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            Save();
            return entity;
        }

        public T Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            Save();
            return entity;
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsQueryable();
        }

        public T? GetById(int id)
        {
            var entity = _context.Set<T>().Find(id);
            return entity;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
