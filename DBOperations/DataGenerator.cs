using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                    Id = 1,
                    Title = "Dune",
                    GenreId = 1,
                    PageCount = 800,
                    PublishDate = DateTime.Now
                    },
                    new Book{
                    Id = 2,
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