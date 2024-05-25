using ElmTest.Domain.Dtos;
using ElmTest.Domain.Entities;
using ElmTest.Services.BookService;
using Microsoft.AspNetCore.Mvc;

namespace ElmTest.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {

        private readonly BookService _bookService;


        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }


        [HttpPost]
        [ValidateModel]

        public async Task<ActionResult<IEnumerable<Book>>> GetBooks([FromBody] BookDto bookDto)
        {
            var books = await _bookService.GetBooks(bookDto);
            return Ok(books);
        }


    }
}
