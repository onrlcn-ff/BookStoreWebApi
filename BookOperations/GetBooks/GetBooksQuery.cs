using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreWebApi.Common;
using BookStoreWebApi.DBOperations;

namespace BookStoreWebApi.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDBContext _context;
        public GetBooksQuery(BookStoreDBContext context)
        {
            _context = context;
        }

        public List<BooksViewModel> Handle(){
            var bookList = _context.Books.OrderBy(x => x.Id).ToList<Book>();
            List<BooksViewModel> vm = bookList.Select( book => new BooksViewModel{
                Title = book.Title,
                PageCount = book.PageCount,
                PublishDate = book.PublishDate,
                Genre = ((GenreEnum)book.GenreId).ToString()
            }).ToList();
            return vm;
        }
    }

    public class BooksViewModel{
        public string Title { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
        public string Genre { get; set; }
    }
}