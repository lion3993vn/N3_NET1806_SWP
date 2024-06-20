using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestVerifyOTP.TestVerifyOTP
{
	public class VerifyOTPEntity
	{
		public string Email { get; set; }
		public int OtpCode { get; set; }
		public int ResponseCode { get; set; }
	}
}
