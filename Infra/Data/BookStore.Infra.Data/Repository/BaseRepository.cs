using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Infra.Data.Context;

namespace BookStore.Infra.Data.Repository
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        protected readonly BookStoreContext _dbContext;

        public BaseRepository(BookStoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TEntity Add(TEntity entity)
        {
             TEntity result = _dbContext.Set<TEntity>().Add(entity).Entity;
             _dbContext.SaveChanges();

             return result;
        } 

        public void Update(TEntity entity) 
        {
            _dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
		    _dbContext.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public IEnumerable<TEntity> ListAll() => _dbContext.Set<TEntity>().ToList();

        public TEntity GetById(int id) => _dbContext.Set<TEntity>().Find(id);
    }
}