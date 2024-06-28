using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDeleteBlog
{
    public class BlogEntity
    {
        public string Testcase { get; set; }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int ResponseCode { get; set; }
        public string Message { get; set; }
    }
}
