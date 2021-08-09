using Microsoft.EntityFrameworkCore;
using my_book_store_v1.Data.Models;
using my_book_store_v1.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book_store_v1.Data.Services
{
    public class PublihserService
    {
        #region DI _Context
        private readonly AppDbContext _context;

        public PublihserService(AppDbContext dbContext)
        {
            this._context = dbContext;
        }
        #endregion

        //add new Publisher
        public void Addpublisher(PublisherVM publisher)
        {
            var _publisher = new Publisher()
            {
                Name = publisher.Name,
            };
            _context.Publishers.Add(_publisher);
            _context.SaveChanges();

        }

        public PublihserWithBookAndAuthorVM GetPublihserData(int pubisherId)
        {
            var _PbulihserData = _context.Publishers.Where(x => x.Id == pubisherId)
                .Select(n => new PublihserWithBookAndAuthorVM()
                {
                    Name = n.Name,
                    BookAuthors = n.Books.Select(y => new BookAuthorsVM()
                    {
                        BookName = y.Title,
                        AuthorsName = y.book_Authors.Select(z => z.Author.FullName).ToList()
                    }).ToList()
                }).FirstOrDefault();
            return _PbulihserData;
        }

        public void DeletepublihserById(int id)
        {
            var _publihser = _context.Publishers.FirstOrDefault(x => x.Id == id);
            if (_publihser !=null)
            {
                _context.Publishers.Remove(_publihser);
                _context.SaveChanges();
            }
        }
    }
}
