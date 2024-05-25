using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmTest.Domain.Dtos
{
    public interface IFilterDto
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
