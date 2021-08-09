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
    public class BooksController : ControllerBase
    {
        #region DI _bookServices
        public BookServices _bookServices;
        public BooksController(BookServices bookServices)
        {
            _bookServices = bookServices;
        }
        #endregion

        [HttpGet("get-all-books")]
        public IActionResult GetBooks()
        {
            var books = _bookServices.GetBooks();
            return Ok(books);
        }

        [HttpGet("get-book-by-id/{id}")]
        public IActionResult GetBookById(int id)
        {
            var bookId = _bookServices.GetBookById(id);
            return Ok(bookId);
        }

        [HttpPost("add-book-with-author")]
        public IActionResult AddBook(BookVM book)
        {
            _bookServices.AddBookWithAuthor(book);
            return Ok();
        }

        [HttpPut("update-book-by-id/{id}")]
        public IActionResult UpdateBook(int id,[FromBody] BookVM book)
        {
            var bookUPdated = _bookServices.UpdateBook(id, book);
            return Ok(bookUPdated);
        }

        [HttpDelete("delete-book-by-id/{id}")]
        public IActionResult DeleteBook(int id)
        {
            _bookServices.DeleteBook(id);
            return Ok();
        }


    }
}
