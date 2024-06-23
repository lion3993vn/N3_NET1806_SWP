using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.TestProductManage
{
    public class PaginatedProductCsvEntity
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int ResponseCode { get; set; }
    }
}
