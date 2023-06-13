using AutoMapper;
using BookStoreWebApi.BookOperations.CreateBook;
using BookStoreWebApi.BookOperations.DeleteBook;
using BookStoreWebApi.BookOperations.GetBookDetail;
using BookStoreWebApi.BookOperations.GetBooks;
using BookStoreWebApi.BookOperations.UpdateBook;
using BookStoreWebApi.DBOperations;
using Microsoft.AspNetCore.Mvc;
using static BookStoreWebApi.BookOperations.UpdateBook.UpdateBookCommand;

namespace BookStoreWebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDBContext _context;
        private readonly ILogger<BookController> _logger;
        private readonly IMapper _mapper;

        public BookController(ILogger<BookController> logger, BookStoreDBContext context, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetBooks(){
           GetBooksQuery query = new GetBooksQuery(_context, _mapper);
           var result = query.Handle();
           return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetBookById(int id ){
           GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
           DetailBookViewModel result;

           try
           {
               result =  query.Handle(id); 
           }
           catch (Exception ex)
           {
                return BadRequest(ex.Message);
           }

           return Ok(result);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);

            try
            {
                command.Model = newBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody] UpdateBookModel updateBook)
        {
            UpdateBookCommand update = new UpdateBookCommand(_context, _mapper);

            try
            {
                update.Model = updateBook;
                update.Handle(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
         public IActionResult DeleteBook(int id)
        {
           DeleteBookCommand delete = new DeleteBookCommand(_context);

           try
           {
                delete.Handle(id); 
           }
           catch (Exception ex)
           {
                return BadRequest(ex.Message);
           }

            return Ok();

        }
    }
}