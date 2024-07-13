using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.UpdateAccountTest
{
	//cái này dùng để ánh xạ xuống file csv
	public class UpdateAccountEntity
	{
		public string Testcase { get; set; }
		public int TesterId { get; set; }
		public string AccountId { get; set; }
		public string? Fullname { get; set; }
		public int RoleId { get; set; }
		public bool? Status { get; set; }
		public string? Avatar { get; set; }
		public string? PhoneNumber { get; set; }
		public string? MainAddress { get; set; }
		public int ResponseCode { get; set; }
		public string ResponseMessage { get; set; }
	}
}
