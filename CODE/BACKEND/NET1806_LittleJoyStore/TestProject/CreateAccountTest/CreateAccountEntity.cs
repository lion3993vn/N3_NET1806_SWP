﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.CreateAccountTest
{
	//cái này dùng để ánh xạ xuống file csv
	public class CreateAccountEntity
	{
		public string Testcase { get; set; }
		public int CreatorId { get; set; }
		public string? UserName { get; set; }
		public string? Password { get; set; }
		public int RoleId { get; set; }
		public string? FullName { get; set; }
		public string Email { get; set; }
		public string? Avatar { get; set; }
		public string PhoneNumber { get; set; }
		public string? MainAddress { get; set; }
		public int ResponseCode { get; set; }
		public string ResponseMessage { get; set; }
	}
}
