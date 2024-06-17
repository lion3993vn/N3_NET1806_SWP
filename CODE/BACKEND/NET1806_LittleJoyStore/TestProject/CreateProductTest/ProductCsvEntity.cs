using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.CreateProductTest
{
    public class ProductCsvEntity
    {
        public int? Id { get; set; }
        public int? CateId { get; set; }

        public string? ProductName { get; set; }

        public int? Price { get; set; }

        public string? Description { get; set; }

        public int? Weight { get; set; }

        public bool? IsActive { get; set; }

        public int Quantity { get; set; }

        public string? Image { get; set; }

        public int? AgeId { get; set; }

        public int? OriginId { get; set; }

        public int? BrandId { get; set; }

        public string? UnsignProductName { get; set; }

        public int? ResponseCode { get; set; }

        public string? ErrorMessage { get; set; }
    }

}
