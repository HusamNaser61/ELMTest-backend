using ElmTest.Domain.Dtos;
using ElmTest.Domain.Entities;
using ElmTest.Domain.Interfaces;

namespace ElmTest.Services.BookService
{
    public class BookService
    {
        private readonly IBookRepository _BookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _BookRepository = bookRepository;
        }

        public async Task<IEnumerable<Book>> GetBooks(BookDto bookDto)
        {
            int totalBookCount = await _BookRepository.GetTotalBookCount();
            int totalPages = (int)Math.Ceiling(totalBookCount / (double)bookDto.PageSize);

            return await _BookRepository.GetBooks(bookDto);
        }
    }
}
