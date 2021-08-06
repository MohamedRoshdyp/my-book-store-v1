using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using my_book_store_v1.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book_store_v1.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            //Scope with App Service
            using (var ServiceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //get db
                var context = ServiceScope.ServiceProvider.GetService<AppDbContext>();

                if (!context.Books.Any())
                {
                    context.Books.AddRange(new Books()
                    {

                        Title = "First Books",
                        Description = "First Description",
                        CoverUrl = "Https...",
                        DatedAdded = DateTime.Now.AddDays(-5),
                        DateRead = DateTime.Now,
                        Genre = "Comdey",
                        IsRead = true,
                        Rate = 6
                    },

                     new Books()
                     {

                         Title = "second Books",
                         Description = "second Description",
                         CoverUrl = "Https...",
                         DatedAdded = DateTime.Now.AddDays(-5),
                         Genre = "Comdey",
                         IsRead = false
                     });
                    context.SaveChanges();
                }
            }
        }
    }
}
