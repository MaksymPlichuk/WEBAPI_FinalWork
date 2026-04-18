using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBAPI_FinalWork.BLL.Dtos.Pagination
{
    public class PaginationInfoDto<T>
    {
        public int Page { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public int PageCount { get; set; } = 1;
        public int TotalCount { get; set; } = 0;
        public IEnumerable<T> Data { get; set; } = [];
    }
}
