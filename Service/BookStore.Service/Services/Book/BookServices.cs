using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Infra.Data.Context;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;

namespace BookStore.Service.Services
{
    public class BookServices : BaseService<Book>
    {
        public BookServices(IRepository<Book> repository) : base(repository)
        {
            
        }
    }
}