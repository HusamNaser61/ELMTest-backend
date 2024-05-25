using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmTest.Domain.Dtos
{
    public class BookDto: IFilterDto
    {

        public string BookTitle { get; set; }
        public string BookDescription { get; set; }
        public string Author { get; set; }
        [DataType(DataType.Date)] // Specifies the data type as Date
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public string PublishDate { get; set; }

        [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
        [Range(1, int.MaxValue, ErrorMessage = "The value must be at least 1.")]
        public int? PageNumber { get; set; }

        [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
        [Range(1, int.MaxValue, ErrorMessage = "The value must be at least 1.")]
        public int? PageSize { get; set; }
    }


}
