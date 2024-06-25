using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.DemoTest
{
    //cái này dùng để ánh xạ xuống file csv
    public class LoginCsvEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int ResponseCode { get; set; }
    }
}
