using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using FluentValidation;
using BookStore.Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Service.Services
{
    public class BaseService<T> : IService<T> where T : EntityBase
    {
        
        private IRepository<T> repository;

        public BaseService(IRepository<T> repository)
        {        
            this.repository = repository;
        }

        public T Post<V>(T entity) where V : AbstractValidator<T>
        {
            Validate(entity, Activator.CreateInstance<V>());

            return repository.Add(entity);
        }

        public T Put<V>(T entity) where V : AbstractValidator<T>
        {
            Validate(entity, Activator.CreateInstance<V>());

            repository.Update(entity);
            return entity;
        }

        public void Delete(int id)
        {
            if (id == 0)
                throw new ArgumentException("The id can't be zero.");

            repository.Delete(repository.GetById(id));
        }

        public IList<T> Get() => repository.ListAll().ToList<T>();

        public T Get(int id)
        {
            if (id == 0)
                throw new ArgumentException("The id can't be zero.");

            return repository.GetById(id);
        }

        private void Validate(T entity, AbstractValidator<T> validator)
        {
            if (entity == null)
                throw new Exception("Registros não detectados!");

            validator.ValidateAndThrow(entity);
        }
    }
}