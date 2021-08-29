using Microsoft.EntityFrameworkCore;
using my_book_store_v1.Data.Models;
using my_book_store_v1.Data.Paging;
using my_book_store_v1.Data.ViewModels;
using my_book_store_v1.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
        public Publisher Addpublisher(PublisherVM publisher)
        {

            if (StringStartWithNumber(publisher.Name))
            {
                throw new PublihserNameException("Start With a digit number", publisher.Name);
            }

            var _publisher = new Publisher()
            {
                Name = publisher.Name,
            };
            _context.Publishers.Add(_publisher);
            _context.SaveChanges();
            return _publisher;
        }

        public List<Publisher> GetAllPublihser(string orderBy, string searchValue,int ? pageNumber,int ? pageSize) 
        { 
            var pulihserList = _context.Publishers.OrderBy(x=>x.Name.ToLower()).ToList();

            //Sorting
            if(!string.IsNullOrEmpty(orderBy))
            {
                switch (orderBy)
                {
                    case "name_desc":
                        pulihserList = pulihserList.OrderByDescending(x => x.Name.ToLower()).ToList();
                        break;

                        case "id_desc":
                        pulihserList = pulihserList.OrderByDescending(x => x.Id).ToList();
                        break;

                    default:
                        break;
                }

             
            }
            //Filtring
            if (!string.IsNullOrEmpty(searchValue))
            {
                pulihserList = pulihserList.Where(x => x.Name.Contains(searchValue,StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            //Paging
            //int pageSize = 3;
            pulihserList = PagedList<Publisher>.ToPagedList(pulihserList.AsQueryable(), pageNumber ?? 1, pageSize??5);
            
           
            return pulihserList;

        } 
       

        public Publisher GetPublisherById(int pubisherId)=> _context.Publishers.FirstOrDefault(x => x.Id == pubisherId);
             
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
            else
            {
                throw new Exception($"This id {id} not Found!");
            }
          
        }


        private bool StringStartWithNumber(string name)=>Regex.IsMatch(name, @"^\d");


    }
}
