using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ElmTest.Domain.Entities
{
    public class Book
    {

        public long BookId { get; set; }
        public string BookInfo { get; set; }
        public DateTime LastModified { get; set; }

        public string BookTitle { get; set; }

        public string Author { get; set; }
        public DateTime PublishDate { get; set; }

        
        private string _CoverBase64;

        public string CoverBase64
        {
            get
            {
                if (string.IsNullOrEmpty(_CoverBase64) && !string.IsNullOrEmpty(BookInfo))
                {
                    _CoverBase64 = DeserializeBookTitle("CoverBase64");
                }
                return _CoverBase64;
            }
            set
            {
                _CoverBase64 = value;
            }
        }

        private string _BookDescription;

        public string BookDescription
        {
            get
            {
                if (string.IsNullOrEmpty(_BookDescription) && !string.IsNullOrEmpty(BookInfo))
                {
                    _BookDescription = DeserializeBookTitle("BookDescription");
                }
                return _BookDescription;
            }
            set
            {
                _BookDescription = value;
            }
        }




        private string DeserializeBookTitle(string property)
        {
            try
            {
                var jsonDoc = JsonDocument.Parse(BookInfo);
                if (jsonDoc.RootElement.TryGetProperty(property, out JsonElement titleElement))
                {
                    return titleElement.GetString();
                }
            }
            catch (JsonException ex)
            {
                // Handle JSON parsing error
                Console.WriteLine($"Error parsing BookInfo: {ex.Message}");
            }
            return null;
        }

    }
}
