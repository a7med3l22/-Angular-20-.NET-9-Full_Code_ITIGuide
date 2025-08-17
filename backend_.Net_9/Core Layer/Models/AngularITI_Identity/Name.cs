using Core_Layer.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Models.AngularITI_Identity
{
	public class Name:BaseClass
	{
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
	}
}
