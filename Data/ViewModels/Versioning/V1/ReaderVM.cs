using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book_store_v1.Data.ViewModels.Versioning.V1
{
    public class ReaderVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public bool IsRead { get; set; }
    }
}
