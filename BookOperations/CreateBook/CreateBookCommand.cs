using BookStoreWebApi.DBOperations;

namespace BookStoreWebApi.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        private readonly BookStoreDBContext _context;
        public CreateBookModel Model {get; set;}

        public CreateBookCommand(BookStoreDBContext context)
        {
            _context = context;
        }

        public void Handle(){

            var book = _context.Books.SingleOrDefault(book => book.Title == Model.Title);

            if(book is not null)
                throw new InvalidOperationException("Eklemeye Çalıştığınız Kitap Mevcut");

            book = new Book{
                Title = Model.Title,
                PageCount = Model.PageCount,
                PublishDate = Model.PublishDate,
                GenreId = Model.GenreId
            };

            _context.Books.Add(book);
            _context.SaveChanges();
        }
    }

    public class CreateBookModel{
        public string Title { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
        public int GenreId { get; set; }
    }
}