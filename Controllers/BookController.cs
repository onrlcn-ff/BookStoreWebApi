using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BookStoreWebApi.DBOperations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookStoreWebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDBContext _context;
        private readonly ILogger<BookController> _logger;

        public BookController(ILogger<BookController> logger, BookStoreDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public List<Book> GetBooks(){
            var bookList = _context.Books.OrderBy(x => x.Id).ToList<Book>();
            return bookList;
        }
        [HttpGet("{id}")]
        public Book GetBookById(int id ){
            var book = _context.Books.Where(book => book.Id == id).SingleOrDefault();
            return book;
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook)
        {
            var book = _context.Books.SingleOrDefault(book => book.Title == newBook.Title);

            if(book is not null)
                return BadRequest();

            _context.Books.Add(newBook);
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody] Book updateBook)
        {
            var book = _context.Books.SingleOrDefault(book => book.Id == id);

            if(book is null)
                return BadRequest();

            book.GenreId = updateBook.GenreId != default ? updateBook.GenreId : book.GenreId;
            book.Title = updateBook.Title!= default ? updateBook.Title: book.Title;
            book.PublishDate= updateBook.PublishDate!= default ? updateBook.PublishDate: book.PublishDate;
            book.PageCount = updateBook.PageCount!= default ? updateBook.PageCount: book.PageCount;

            return Ok();
        }

        [HttpDelete("{id}")]
         public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.SingleOrDefault(book => book.Id == id);
            
            if(book is null)
                return BadRequest();

           _context.Books.Remove(book);
            return Ok();

        }
    }
}