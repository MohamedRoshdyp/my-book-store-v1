using Marvin.Cache.Headers;
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
    public class AuthorsController : ControllerBase
    {
        #region DI _authorService
        public AuthorService _authorService;
        public AuthorsController(AuthorService authorService)
        {
            _authorService = authorService;
        }
        #endregion

        [HttpPost("add-author")]
        public IActionResult AddAuthor([FromBody] AuthorVM author)
        {
            _authorService.AddAuthor(author);
            return Ok();
        }

        [HttpCacheExpiration(CacheLocation =CacheLocation.Public,MaxAge =70)]
        [HttpCacheValidation(MustRevalidate =false)]
        //[ResponseCache(CacheProfileName = "120SecondsDuration")]
        [HttpGet("get-all-author")]
        public IActionResult GetAuthor()
        {
            var _allAuthors = _authorService.GetAllAuthors();
            return Ok(_allAuthors);
        }


        //Test ETag Http
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 90)]
        [HttpCacheValidation(MustRevalidate = false)]
        [HttpGet("etag-test")]
        public IActionResult ETag()
        {
            return Ok("This is Etag3");
        }


        //[ResponseCache(CacheProfileName = "120SecondsDuration")]
        [HttpGet("get-author-with-book-by-id/{ID}")]
        public IActionResult GetAuthorwithBook(int ID)
        {

            try
            {
                var result = _authorService.GetAuthorWithBookVM(ID);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
           
        }

    }
}
