using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Domain.Entities;

namespace BookStore.Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        TEntity Add(TEntity item);
        void Delete(TEntity item);  
        void Update(TEntity item);   
        IEnumerable<TEntity> ListAll();
        TEntity GetById(int id);        
    }
}