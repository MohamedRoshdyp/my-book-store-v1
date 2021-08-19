using Microsoft.EntityFrameworkCore;
using my_book_store_v1.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book_store_v1.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

        //Fluent Api
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //configure book-author- books
            modelBuilder.Entity<Book_Author>().HasOne(b => b.Books).WithMany(ba => ba.book_Authors).HasForeignKey(bi => bi.BooksId);
            //configure book-author- author
            modelBuilder.Entity<Book_Author>().HasOne(b => b.Author).WithMany(ba => ba.book_Authors).HasForeignKey(bi => bi.AuthorId);
            //Configre LogID
            modelBuilder.Entity<Log>().HasKey(o => o.Id);

        }

        public DbSet<Books> Books { get; set; }
        public DbSet<Book_Author> Book_Authors { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<Log> Logs { get; set; }


    }
}
