using Microsoft.EntityFrameworkCore;

namespace BookStoreWebApi.DBOperations
{
    public class DataGenerator
    {
        
        public static void Initialize(IServiceProvider serviceProvider) 
        {
            using (var context = new BookStoreDBContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDBContext>>()))
            {

                if(context.Books.Any())
                {
                    return;
                }

                context.Books.AddRange(
                    new Book{
                    Title = "Dune",
                    GenreId = 1,
                    PageCount = 800,
                    PublishDate = DateTime.Now
                    },
                    new Book{
                    Title = "DuneChildren",
                    GenreId = 1,
                    PageCount = 900,
                    PublishDate = DateTime.Now
                    });

                context.SaveChanges();
            }
        }
    }
}