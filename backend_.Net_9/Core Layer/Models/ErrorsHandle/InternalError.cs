using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core_Layer.Models.ErrorsHandle
{
	public class InternalError:HandleErrors
	{
		public InternalError(string message,string? stackTrace = null) :base(500, message)
		{
			StackTrace = stackTrace;
		}
		//[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] //By Default, this property will be ignored when it is null during serialization
		public string? StackTrace { get; }

	}
}
