using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUpdateBlog
{
    public class BlogEntity
    {
        public string Testcase { get; set; }

        public int Id { get; set; }
        public int UserId { get; set; }

        public string? Title { get; set; }

        public string? Banner { get; set; }

        public string? Content { get; set; }

        public int ResponseCode { get; set; }
        public string Message { get; set; }
    }
}
