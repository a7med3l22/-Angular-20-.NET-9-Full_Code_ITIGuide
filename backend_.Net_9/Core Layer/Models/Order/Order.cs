using Core_Layer.Models.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Models.Order
{
	//--Create class Order:BaseClass [ClientEmail-OrderDate-OrderState-ShippingAddress-deliveryMethod-OrderItems-SubTotal-GetTotal-PaymentIntentId]
	//--Create enum OrderState[Pending-PaymentReceived-PaymentFailed]
	//--Create class ShippingAddress[FirstName-LastName-Street-City-Country]
	//--Create class DeliveryMethod : BaseClass[ShortName - Description - Cost - DeliveryTime]
	//--Create class OrderItem : BaseClass[ProductItemOrdered - Price - Quantity] 
	//--Create class ProductItemOrdered Contain[ProductId, ProductName, PictureUrl]
	public class Order:BaseClass
	{
		public string ClientEmail { get; set; } = null!;
		public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
		public OrderState orderState { get; set; } = OrderState.Pending;
		public ShippingAddress shippingAddress { get; set; } = null!; //ShippingAddress is [owned] to Order
		public DeliveryMethod? deliveryMethod { get; set; } = null!;   //DeliveryMethod is Principal, Order is Dependent // ممكن معملش DeliveryMethod_Id  // وهو طالما ال دليفيري ميثود نلابول هيبقي ال فورن كي بتاعها نلابول تلقائي
		public int? DeliveryMethod_Id { get; set; } // عملته كده علشان لما امسح ال دليفيري ميثود ال ديليفري ميثود اي جوه ال اوردر تبقي نلابول 
		public ICollection<OrderItem> orderItems { get; set; } = new HashSet<OrderItem>();//OrderItem is Dependent, Order is Principal //✅ HashSet يمنع التكرار تلقائيًا ويحسن الأداء عند البحث.
		public decimal SubTotal { get; set; }
		[NotMapped]
		public decimal GetTotal=> SubTotal+ deliveryMethod?.Cost??0;
		public string PaymentIntentId { get; set; } = null!; // علشان لو مبعتش قيمة ميضربش اكسيبشن

	}
}
/*
✅ الخلاصة النهائية المرتبة:
🔹 One - to - One ➔ لازم تحدد FK يدويًا لتحديد الـ Principal/Dependent.
🔹 One-to-Many ➔ FK في الـ Dependent ضروري (لكن EF Core يضيفه تلقائيًا لو لم تحدده يدويًا).
🔹 Many-to-Many ➔ EF Core يدير الـ FK تلقائيًا بالكامل، لا داعي لتحديدها يدويًا.
*/
