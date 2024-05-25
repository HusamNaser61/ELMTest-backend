using Dapper;
using ElmTest.Domain.Dtos;
using ElmTest.Domain.Entities;
using ElmTest.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.SqlServer.Server;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace ElmTest.Data.BookrRepository
{
    public class BookRepository : IBookRepository
    {
        private readonly IConfiguration _configuration;
        public BookRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<IEnumerable<Book>> GetBooks(BookDto bookDto)
        {
            try
            {

                using (var connection = CreateConnection())
                {
                    var parameters = new DynamicParameters();

                    // List to accumulate conditions for WHERE clause
                    var conditions = new List<string>();

                    // Check if the Author filter is provided
                    if (!String.IsNullOrWhiteSpace(bookDto.Author))
                    {
                        conditions.Add("Author LIKE @Author");
                        parameters.Add("Author", $"%{bookDto.Author}%");
                    }
                    if (!String.IsNullOrWhiteSpace(bookDto.BookTitle))
                    {
                        conditions.Add("BookTitle LIKE @BookTitle");
                        parameters.Add("BookTitle", $"%{bookDto.BookTitle}%");
                    }
                    if (!String.IsNullOrWhiteSpace(bookDto.BookDescription))
                    {
                        conditions.Add("BookDescription LIKE @BookDescription");
                        parameters.Add("BookDescription", $"%{bookDto.BookDescription}%");
                    }
                    if (!String.IsNullOrWhiteSpace(bookDto.PublishDate))
                    {
                        DateTime dateTime = DateTime.ParseExact(bookDto.PublishDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        conditions.Add("PublishDate = @PublishDate");
                        parameters.Add("PublishDate", dateTime);
                    }

                    var filters = "";
                    // Append WHERE clause if conditions exist
                    if (conditions.Any())
                    {
                        filters = " WHERE " + string.Join(" AND ", conditions);
                    }
                    
                    string sql = @$"SELECT BookId, BookInfo ,BookTitle,BookDescription,Author,PublishDate
                    FROM Book (NOLOCK)
                    {filters}
                    ORDER BY BookId
                    OFFSET @Offset ROWS
                    FETCH NEXT @PageSize ROWS ONLY";


                    parameters.Add("Offset", (bookDto.PageNumber - 1) * bookDto.PageSize);
                    parameters.Add("PageSize", bookDto.PageSize);

                    return await connection.QueryAsync<Book>(sql, parameters);
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<int> GetTotalBookCount()
        {
            using (var connection = CreateConnection())
            {
                string sql = "SELECT COUNT(*) FROM Book (NOLOCK)";

                return await connection.ExecuteScalarAsync<int>(sql);
            }
        }
    }
}
