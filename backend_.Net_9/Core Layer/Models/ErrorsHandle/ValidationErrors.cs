using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Models.ErrorsHandle
{
	public class ValidationErrors: HandleErrors
	{
		public ValidationErrors(List<string> Errors):base(400,"Bad Request,Validation Error Occurred")
		{
			this.Errors=Errors;
		}
		public List<string> Errors { get; } = new();
	}
}
