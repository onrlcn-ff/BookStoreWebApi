using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreWebApi.DBOperations;

namespace BookStoreWebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {

        private readonly BookStoreDBContext _context;
        public UpdateBookModel Model;

        public UpdateBookCommand(BookStoreDBContext context)
        {
           _context = context; 
        }

        public void Handle(int id){
            var book = _context.Books.SingleOrDefault(book => book.Id == id);

            if(book is null)
                throw new InvalidOperationException("Güncellenecek Kitap bulunamadi");

            book.Title = Model.Title!= default ? Model.Title: book.Title;
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            _context.SaveChanges();
        }

        public class UpdateBookModel{
        public string Title { get; set; }
        public int GenreId { get; set; }
    }
    }
}