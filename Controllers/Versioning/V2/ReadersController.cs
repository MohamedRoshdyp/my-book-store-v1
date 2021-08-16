using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_book_store_v1.Data.ViewModels.Versioning.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book_store_v1.Controllers.Versioning.V2
{
    //[ApiVersion("1.0")]
    //[ApiVersion("1.4")]
    //[ApiVersion("1.8", Deprecated = true)]
    //[Route("api/v{version:apiversion}/[controller]")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReadersController : ControllerBase
    {
        //Static Data

        static List<ReaderVM> _readers = new List<ReaderVM>()
        {
            new ReaderVM(){Id=1,Name="Tom",Address="USA",IsRead=true,borrowDate = DateTime.Now.AddDays(-3),returnDate = DateTime.Now.AddDays(6)},
            new ReaderVM(){Id=1,Name="Micle",Address="KSA",IsRead=true,borrowDate = DateTime.Now.AddDays(-5),returnDate = DateTime.Now.AddDays(9)},
            new ReaderVM(){Id=1,Name="Mark",Address="EGP",IsRead=true,borrowDate = DateTime.Now.AddDays(-8),returnDate = DateTime.Now.AddDays(2)},
            new ReaderVM(){Id=1,Name="Stev",Address="USA",IsRead=true,borrowDate = DateTime.Now.AddDays(-10),returnDate = DateTime.Now.AddDays(3)},
            new ReaderVM(){Id=1,Name="Omar",Address="USA",IsRead=true,borrowDate = DateTime.Now.AddDays(-20),returnDate = DateTime.Now.AddDays(1)}

        };


        [HttpGet("get-all-reader")]
        public IActionResult GetV1()
        {
            return Ok("This is V1.0");
        }

        [HttpGet("get-all-reader"),MapToApiVersion("1.4")]
        public IActionResult GetAllReader()
        {
            return Ok(_readers);
        }

        [HttpGet("get-all-reader"),MapToApiVersion("1.8")]
        public IActionResult GetV18()
        {
            return Ok("This is V1.8");
        }

        [HttpGet("get-reader-by-id/{id}")]
        public IActionResult GetReaderById(int id)
        {
            var _readerId = _readers.FirstOrDefault(o => o.Id == id);
            return Ok(_readerId);
        }

    }
}
