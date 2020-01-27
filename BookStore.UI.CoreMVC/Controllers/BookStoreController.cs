using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using BookStore.Service.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookStore.UI.CoreMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookStoreController : ControllerBase
    {
        private readonly ILogger<BookStoreController> logger;
        private readonly IService<Book> bookService;
        public BookStoreController(ILogger<BookStoreController> logger, IService<Book> _bookService)
        {
            this.logger = logger;
            this.bookService = _bookService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAll()
        {
            logger.LogInformation("Retriving books.");
            var ModelforView = bookService.Get();
            logger.LogInformation($"Retrived {ModelforView.Count()} books.");

            return Ok(ModelforView?.OrderBy(book => book.Title).ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Book> Get(int id)
        {
            if (ModelState.IsValid)
            {
                logger.LogInformation($"Getting book with id '{id}'.");
                var BookForView = bookService.Get(id);
                if (BookForView.IsValid())
                    return Ok(BookForView);
            }

            return BadRequest();
        }

        [HttpPost]
        public void Post([FromBody] Book bookModel)
        {
            if (ModelState.IsValid)
            {
                logger.LogInformation($"Including book with title: {bookModel.Title}");
                Book insertedBook = bookService.Post<BookValidator>(bookModel);
            }
        }

        [HttpPut("{id}")]
        public void Put(int Id, [FromBody] Book bookModel)
        {
            if (ModelState.IsValid)
            {
                logger.LogInformation($"Updating book with id: {Id}.");
                Book book = bookService.Get(Id);
                if (book.IsValid())
                {
                    book.Title = bookModel.Title;
                    book.ReleaseDate = bookModel.ReleaseDate;
                    book.Price = bookModel.Price;
                    book.AuthorName = bookModel.AuthorName;

                    var LastUpdatedBook = bookService.Put<BookValidator>(book);
                    if (LastUpdatedBook.IsValid())
                    {
                        logger.LogInformation($"Updated book with id: {Id}.");
                    }
                }
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            logger.LogInformation($"Deleting book with id: {id}.");
            if (id <= 0)
                NotFound();

            Book book = bookService.Get(id);
            if (book.IsValid())
            {
                bookService.Delete(id);
                logger.LogInformation($"Deleted book with id: {id}.");
            }
        }

    }
}