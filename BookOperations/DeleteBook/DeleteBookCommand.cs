using BookStoreWebApi.DBOperations;

namespace BookStoreWebApi.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly BookStoreDBContext _context;

        public DeleteBookCommand(BookStoreDBContext context)
        {
            _context = context;
        }

        public void Handle(int id)
        {
             var book = _context.Books.SingleOrDefault(book => book.Id == id);
            
            if(book is null)
            {
                throw new InvalidOperationException("Sileceğiniz Kitap Bulunamadi");
            }

           _context.Books.Remove(book);
            _context.SaveChanges();
        }
    }
}