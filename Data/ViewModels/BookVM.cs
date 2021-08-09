using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book_store_v1.Data.ViewModels
{
    public class BookVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public int? Rate { get; set; }
        public string CoverUrl { get; set; }
        public string Genre { get; set; }

        public int PublihserId { get; set; }
        public List<int> AuthorIds { get; set; }
    }

    public class BookwithAuthorVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public int? Rate { get; set; }
        public string CoverUrl { get; set; }
        public string Genre { get; set; }

        public string PublihserName { get; set; }
        public List<string> AuthorsName { get; set; }
    }
}
