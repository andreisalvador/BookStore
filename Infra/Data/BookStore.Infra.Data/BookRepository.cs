using System;
using BookStore.Domain.Entities;
using BookStore.Infra.Data.Context;
using BookStore.Infra.Data.Repository;

namespace BookStore.Infra.Data
{
    public class BookRepository : BaseRepository<Book>
    {
        public BookRepository(BookStoreContext dbContext) : base(dbContext)
        {
        }
    }
}
