using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book_store_v1.Data.Paging
{
    public class PagedList<T>:List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; set; }

        public bool HasPrevious => CurrentPage > 1;

        public bool HasNext => CurrentPage < TotalPages;



        public PagedList(List<T> items, int count,int pageNumber, int pageSize)
        {
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            AddRange(items);
        }

        public static PagedList<T> ToPagedList(IQueryable<T> source,int pageNumber,int pagesize)
        {


            var count = source.Count();

            //pageNumber = 3
            //pageSize = 5
            //2*5 = 10 

            var items = source.Skip((pageNumber - 1) * pagesize).Take(pagesize).ToList();
            return new PagedList<T>(items, count,pageNumber, pagesize);
        }
    }
}
