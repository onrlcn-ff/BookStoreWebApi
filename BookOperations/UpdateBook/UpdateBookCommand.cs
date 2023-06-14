using AutoMapper;
using BookStoreWebApi.DBOperations;

namespace BookStoreWebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {

        private readonly BookStoreDBContext _context;
        private readonly IMapper _mapper;

        public UpdateBookModel Model;
        public int Id { get; set; }

        public UpdateBookCommand(BookStoreDBContext context, IMapper mapper)
        {
            _mapper = mapper;
           _context = context; 
        }

        public void Handle(){
            var book = _context.Books.SingleOrDefault(book => book.Id == Id);

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