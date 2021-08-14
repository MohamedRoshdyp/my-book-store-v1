using my_book_store_v1.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book_store_v1.Data.ViewModels
{
    public class CustomeActionResultVM
    {
        public Exception Exception { get; set; }
        public Publisher Publisher { get; set; }
    }
}
