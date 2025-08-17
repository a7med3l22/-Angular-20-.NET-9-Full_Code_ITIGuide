using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Models.AngularITI_Identity
{
	public class LoginResultDto
	{
		public string? error { get; set; }
		public string? token { get; set; }
	}
}
