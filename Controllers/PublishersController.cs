using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_book_store_v1.Data.Services;
using my_book_store_v1.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book_store_v1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        #region DI _publisherService
        private readonly PublihserService _publihserService;

        public PublishersController(PublihserService publihserService)
        {
           _publihserService = publihserService;
        }
        #endregion

        //Add Publisher
        [HttpPost("add-publihser")]
        public IActionResult AddPublisher([FromBody] PublisherVM publisher)
        {
            _publihserService.Addpublisher(publisher);
            return Ok();
        }

        [HttpGet("get-publihser-book-with-author-by-id/{id}")]
        public IActionResult GetpubliserData(int id)
        {
            var response = _publihserService.GetPublihserData(id);
            return Ok(response);
        }

        [HttpDelete("delete-publisher-by-id/{id}")]
        public IActionResult DeletePublisherById(int id)
        {
            _publihserService.DeletepublihserById(id);
            return Ok();
        }

     

    }
}
