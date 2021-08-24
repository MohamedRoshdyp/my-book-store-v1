using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using my_book_store_v1.ActionResults;
using my_book_store_v1.Data.Models;
using my_book_store_v1.Data.Services;
using my_book_store_v1.Data.ViewModels;
using my_book_store_v1.Exceptions;
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
        private readonly ILogger<PublishersController> _logger;
        public PublishersController(PublihserService publihserService, ILogger<PublishersController> logger)
        {
            _publihserService = publihserService;
            _logger = logger;
        }
        #endregion

        //Add Publisher
        [HttpPost("add-publihser")]
        public IActionResult AddPublisher([FromBody] PublisherVM publisher)
        {
            try
            {
                var newpublihser = _publihserService.Addpublisher(publisher);
                return Created(nameof(AddPublisher), newpublihser);
            }
            catch (PublihserNameException ex)
            {
                return BadRequest($"{ex.Message},pulihser name is {ex.PulisherName}");
            }

            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        
        [HttpGet("get-all-publihser")]
        public IActionResult GetAllPublihser(string orderBy,string searchValue,int pageNumber,int pageSize)
        {
            //throw new Exception("This is come from GetAllPublihser()");
            try
            {
                _logger.LogInformation("This log is come from GetAllPublihser()");
                var _result = _publihserService.GetAllPublihser(orderBy,searchValue,pageNumber,pageSize);
                return Ok(_result);
            }
            catch (Exception)
            {

                return BadRequest("Can't Find Any Publihsers");
            }
        }

        [Authorize]
        [HttpGet("get-publisher-by-id/{id}")]
        public IActionResult GetpublisherById(int id)
        {
            var response = _publihserService.GetPublisherById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);

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

            try
            {
                _publihserService.DeletepublihserById(id);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest($"The Message is {ex.Message}");
            }





        }

    }

}