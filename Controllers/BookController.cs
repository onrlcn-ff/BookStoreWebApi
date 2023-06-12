using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookStoreWebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private static List<Book> BookList = new List<Book>(){
            new Book{
            Id = 1,
            Title = "Dune",
            GenreId = 1,
            PageCount = 800,
            PublishDate = DateTime.Now
            },
            new Book{
            Id = 2,
            Title = "DuneChildren",
            GenreId = 1,
            PageCount = 900,
            PublishDate = DateTime.Now
            }
        };
        private readonly ILogger<BookController> _logger;

        public BookController(ILogger<BookController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Book> GetBooks(){
            var bookList = BookList.OrderBy(x => x.Id).ToList<Book>();
            return bookList;
        }
        [HttpGet("{id}")]
        public Book GetBookById(int id ){
            var book = BookList.Where(book => book.Id == id).SingleOrDefault();
            return book;
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook)
        {
            var book = BookList.SingleOrDefault(book => book.Title == newBook.Title);

            if(book is not null)
                return BadRequest();

            BookList.Add(newBook);
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody] Book updateBook)
        {
            var book = BookList.SingleOrDefault(book => book.Id == id);

            if(book is null)
                return BadRequest();

            book.GenreId = updateBook.GenreId != default ? updateBook.GenreId : book.GenreId;
            book.Title = updateBook.Title!= default ? updateBook.Title: book.Title;
            book.PublishDate= updateBook.PublishDate!= default ? updateBook.PublishDate: book.PublishDate;
            book.PageCount= updateBook.PageCount!= default ? updateBook.PageCount: book.PageCount;

            return Ok();
        }

        [HttpDelete("{id}")]
         public IActionResult DeleteBook(int id)
        {
            var book = BookList.SingleOrDefault(book => book.Id == id);
            
            if(book is null)
                return BadRequest();
                
           BookList.Remove(book);
            return Ok();
        }
    }
}