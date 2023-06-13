using AutoMapper;
using BookStoreWebApi.Common;
using BookStoreWebApi.DBOperations;

namespace BookStoreWebApi.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly BookStoreDBContext _context;
        private readonly IMapper _mapper;

        public GetBookDetailQuery(BookStoreDBContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public DetailBookViewModel Handle(int id){
            var book = _context.Books.Where(book => book.Id == id).SingleOrDefault();

            if(book is null)
                throw new ("Bu kitap bulunamadi");

            var model = _mapper.Map<DetailBookViewModel>(book);

            return model;
        }
    }
    public class DetailBookViewModel{
        public string Title { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
        public string Genre { get; set; }
    }
}