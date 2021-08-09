using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book_store_v1.Data.Models
{
    public class Book_Author
    {
        public int Id { get; set; }

        //Navaigation property

        public int BooksId { get; set; }
        public Books Books { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
