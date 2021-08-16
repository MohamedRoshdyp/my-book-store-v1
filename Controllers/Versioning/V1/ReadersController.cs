using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_book_store_v1.Data.ViewModels.Versioning.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book_store_v1.Controllers.Versioning.V1
{
    
    [ApiVersion("1.0")]
    //[Route("api/[controller]")]
    [Route("api/v{version:apiversion}/[controller]")]
    [ApiController]
    public class ReadersController : ControllerBase
    {
        //Static Data

        static List<ReaderVM> _readers = new List<ReaderVM>()
        {
            new ReaderVM(){Id=1,Name="Jone",Address="USA",IsRead=true},
            new ReaderVM(){Id=1,Name="Sam",Address="KSA",IsRead=true},
            new ReaderVM(){Id=1,Name="Karma",Address="EGP",IsRead=false},
            new ReaderVM(){Id=1,Name="Jo",Address="USA",IsRead=true},
            new ReaderVM(){Id=1,Name="Anglina",Address="USA",IsRead=false}

        };


        [HttpGet("get-all-reader")]
        public IActionResult GetAllReader()
        {
            return Ok(_readers);
        }

        [HttpGet("get-reader-by-id/{id}")]
        public IActionResult GetReaderById(int id)
        {
            var _readerId = _readers.FirstOrDefault(o => o.Id == id);
            return Ok(_readerId);
        }

    }
}
