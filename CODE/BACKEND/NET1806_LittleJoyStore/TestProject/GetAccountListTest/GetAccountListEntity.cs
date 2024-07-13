using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.GetAccountListTest
{
	//cái này dùng để ánh xạ xuống file csv
	public class GetAccountListEntity
	{
		public string Testcase { get; set; }
		public int TesterId { get; set; }
		public int? RoleId { get; set; }
		public bool? Status { get; set; }
		public int ResponseCode { get; set; }
	}
}
