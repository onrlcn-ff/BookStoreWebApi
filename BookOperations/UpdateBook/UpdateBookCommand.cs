using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStoreWebApi.DBOperations;

namespace BookStoreWebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {

        private readonly BookStoreDBContext _context;
        private readonly IMapper _mapper;

        public UpdateBookModel Model;

        public UpdateBookCommand(BookStoreDBContext context, IMapper mapper)
        {
            _mapper = mapper;
           _context = context; 
        }

        public void Handle(int id){
            var book = _context.Books.SingleOrDefault(book => book.Id == id);

            if(book is null)
                throw new InvalidOperationException("Güncellenecek Kitap bulunamadi");

            _mapper.Map(Model, book); 
          
            _context.SaveChanges();
        }

        public class UpdateBookModel{
        public string Title { get; set; }
        public int GenreId { get; set; }
    }
    }
}