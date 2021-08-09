using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book_store_v1.Data.ViewModels
{
    public class PublisherVM
    {
        public string Name { get; set; }
    }

    public class PublihserWithBookAndAuthorVM
    {
        public string Name { get; set; }

        public List<BookAuthorsVM> BookAuthors { get; set; }


    }

    public class BookAuthorsVM
    {
        public string BookName { get; set; }
        public List<string> AuthorsName { get; set; }
    }
}
