using BookStoreWebApi.Common;
using BookStoreWebApi.DBOperations;

namespace BookStoreWebApi.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly BookStoreDBContext _context;

        public GetBookDetailQuery(BookStoreDBContext context)
        {
            _context = context;
        }

        public DetailBookViewModel Handle(int id){
            var book = _context.Books.Where(book => book.Id == id).SingleOrDefault();

            if(book is null)
                throw new ("Bu kitap bulunamadi");

            var model = new DetailBookViewModel{
               Title = book.Title,
               PageCount = book.PageCount,
               PublishDate = book.PublishDate,
               GenreId = ((GenreEnum)book.GenreId).ToString()
            };

            return model;
        }
    }
    public class DetailBookViewModel{
        public string Title { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
        public string GenreId { get; set; }
    }
}