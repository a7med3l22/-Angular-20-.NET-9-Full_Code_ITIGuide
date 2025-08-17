using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Models.ErrorsHandle
{
	public class HandleErrors

	{
		public HandleErrors(int code, string? message = null)
		{
			Code = code;

			var reasonCode = ReasonPhrases.GetReasonPhrase(code);
			Message = message ?? (!string.IsNullOrWhiteSpace(reasonCode) ? reasonCode : "An error occurred");
		}

		public int Code { get;  } //  عاوز اعملها سيت  من جوه ال كونستركستور فقط
		public string? Message { get; }//  عاوز اعملها سيت  من جوه ال كونستركستور فقط

	}
}
