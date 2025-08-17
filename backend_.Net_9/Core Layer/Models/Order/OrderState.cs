using System.Runtime.Serialization;

namespace Core_Layer.Models.Order
{
	public enum OrderState
	{
		Pending,
		[EnumMember(Value = "Payment Success")] // هيظهر ف الفرونت كده وهيتحفظ ف الداتا بيز ك قيمة عادية
		PaymentReceived,
		[EnumMember(Value = "Payment Failed")]
		PaymentFailed
	}
}