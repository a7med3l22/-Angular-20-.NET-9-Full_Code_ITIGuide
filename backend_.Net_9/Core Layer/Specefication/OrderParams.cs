using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Specefication
{
	public class OrderParams
	{
		public string? Email { get; set; }
		public int? orderId { get; set; }
		public string? PaymentIntentId { get; set; }
	}
}
