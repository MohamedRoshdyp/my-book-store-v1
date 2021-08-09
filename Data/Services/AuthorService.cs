using my_book_store_v1.Data.Models;
using my_book_store_v1.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book_store_v1.Data.Services
{
    public class AuthorService
    {
        #region DI _context
        private AppDbContext _context;
        public AuthorService(AppDbContext context)
        {
            _context = context;
        }
        #endregion

        //add new Author
        public void AddAuthor(AuthorVM author)
        {
            var _author = new Author()
            {
                FullName = author.FullName,
            };
            _context.Authors.Add(_author);
            _context.SaveChanges();
            
        }

        public AuthorWithBookVM GetAuthorWithBookVM(int authorId)
        {
            var _author = _context.Authors.Where(x => x.Id == authorId)
                .Select(y => new AuthorWithBookVM()
                {
                    FullName = y.FullName,
                    BookTitles = y.book_Authors.Select(z => z.Books.Title).ToList()
                }).FirstOrDefault();

            return _author;
        }
    }
}
