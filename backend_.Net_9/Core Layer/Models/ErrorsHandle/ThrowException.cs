using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Models.ErrorsHandle
{
	public class ThrowException:Exception
	{
		public ThrowException(int code, string? myMessage = null)
		{
			Code = code;

			var reasonCode = ReasonPhrases.GetReasonPhrase(code);
			MyMessage = myMessage ?? (!string.IsNullOrWhiteSpace(reasonCode) ? reasonCode : "An error occurred");
		}

		public int Code { get; } //  عاوز اعملها سيت  من جوه ال كونستركستور فقط
		public string? MyMessage { get; }//  عاوز اعملها سيت  من جوه ال كونستركستور فقط
	}
}
/*
 الكود	الاستخدام المناسب
400	لو الخطأ بسبب خطأ من المستخدم (مدخلات غير صحيحة).
401	لو المستخدم غير مصرح له (Unauthorized).
403	لو المستخدم ممنوع من الوصول (Forbidden).
404	لو لم يتم العثور على المورد (Not Found).
500	لو المشكلة داخل السيرفر أو قاعدة البيانات أثناء التنفيذ.


 
 
 */