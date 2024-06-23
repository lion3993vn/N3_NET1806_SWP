using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Test_ProductManage
{
    public class CategoryCsvEntity
    {
        public string CategoryName { get; set; }

        public int ResponseCode { get; set; }

    }

    public class UpdateCategoryCsvEntity
    {
        public int Id { get; set; }

        public string? CategoryName { get; set; }

        public int ResponseCode { get; set; }

    }

    public class DeleteCategoryCsvEntity
    {
        public int Id { get; set; }

        public int ResponseCode { get; set; }

    }
}
