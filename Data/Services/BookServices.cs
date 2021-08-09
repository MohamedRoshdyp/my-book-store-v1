using my_book_store_v1.Data.Models;
using my_book_store_v1.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace my_book_store_v1.Data.Services
{
    public class BookServices
    {
        #region DI _context
        private AppDbContext _context;
        public BookServices(AppDbContext context)
        {
            _context = context;
        }
        #endregion

        //Add New Book 
        public void AddBookWithAuthor(BookVM book)
        {
            var _book = new Books()
            {

                Title = book.Title,
                Description = book.Description,
                CoverUrl = book.CoverUrl,
                Genre = book.Genre,
                IsRead = book.IsRead,
                DatedAdded = DateTime.Now,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rate = book.IsRead ? book.Rate.Value : null,
                PublisherId = book.PublihserId
            };
            _context.Books.Add(_book);
            _context.SaveChanges();

            foreach (var Id in book.AuthorIds)
            {
                var _book_author = new Book_Author()
                {
                    BooksId = _book.Id,
                    AuthorId = Id
                };
                _context.Book_Authors.Add(_book_author);
                _context.SaveChanges();
            }
        }

        //get all books
        public List<Books> GetBooks()
        {
            var books = _context.Books.ToList();
            return books;
        }

        //get book by id

        public Books GetbookById(int id)
        {
            var bookid = _context.Books.FirstOrDefault(x=>x.Id == id);
            return bookid;
            
        }
        public BookwithAuthorVM GetBookById(int id)
        {
            var _bookwithauthor = _context.Books.Where(x => x.Id == id)
                .Select(book => new BookwithAuthorVM()
                {
                    Title = book.Title,
                    Description = book.Description,
                    CoverUrl = book.CoverUrl,
                    Genre = book.Genre,
                    IsRead = book.IsRead,
                    DateRead = book.IsRead ? book.DateRead.Value : null,
                    Rate = book.IsRead ? book.Rate.Value : null,
                    PublihserName = book.Publisher.Name,
                    AuthorsName = book.book_Authors.Select(x => x.Author.FullName).ToList()
                }).FirstOrDefault();


            return _bookwithauthor;
        }

        //updating
        public Books UpdateBook(int id,BookVM book)
        {
            //get bookid
            var _book = GetbookById(id);
            if (_book !=null)
            {

                _book.Title = book.Title;
                _book.Description = book.Description;
                _book.CoverUrl = book.CoverUrl;
                _book.Genre = book.Genre;
                _book.IsRead = book.IsRead;
                _book.DatedAdded = DateTime.Now;
                _book.DateRead = book.IsRead ? book.DateRead.Value : null;
                _book.Rate = book.IsRead ? book.Rate.Value : null;
                _context.SaveChanges();
            }
            return _book;
        }

        //Delteting
        public void DeleteBook(int id)
        {
            //get book id
            var bookId = GetbookById(id);
            if (bookId != null)
            {
                _context.Books.Remove(bookId);
                _context.SaveChanges();

            }
        }


    }
}
