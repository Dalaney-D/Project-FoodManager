using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryBase<T> where T : class
    {
        private readonly FoodManagerContext _context;
        private readonly DbSet<T> _dbSet;
        public RepositoryBase()
        {
            _context = new FoodManagerContext();
            _dbSet = _context.Set<T>();
        }

        public T Get(string id)
        {
            return _dbSet.FirstOrDefault();
        }

        public IQueryable<T> GetAll2()
        {
            return _dbSet;
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }


        public void Create(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
            //_dbSet.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }
    }
}
