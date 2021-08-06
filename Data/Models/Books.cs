using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book_store_v1.Data.Models
{
    public class Books
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsRead { get; set; }
        public DateTime ? DateRead { get; set; }
        public int? Rate { get; set; }
        public string CoverUrl { get; set; }
        public string Genre { get; set; }
        public DateTime DatedAdded { get; set; }
    }
}
