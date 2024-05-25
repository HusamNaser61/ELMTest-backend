using ElmTest.Domain.Dtos;
using ElmTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmTest.Domain.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooks(BookDto bookDto);
        Task<int> GetTotalBookCount();


    }
}
