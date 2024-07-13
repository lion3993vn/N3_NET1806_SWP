using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.DeleteAccountTest
{
	//cái này dùng để ánh xạ xuống file csv
	public class DeleteAccountEntity
	{
		public string Testcase { get; set; }
		public int TesterId { get; set; }
		public string AccountId { get; set; }
		public int ResponseCode { get; set; }
		public string ResponseMessage { get; set; }
	}
}
