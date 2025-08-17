using Core_Layer.Models.Product;

namespace Core_Layer.Models.Identity
{
	public class Address:BaseClass
	{
		public string FirstName { get; set; } = null!;
		public string LastName { get; set; } = null!;
		public string Street { get; set; } = null!;
		public string City { get; set; } = null!;
		public string Country { get; set; } = null!;
		//every address is Must haven by one application user
		public ApplicationUser ApplicationUser { get; set; } = null!;
		public string ApplicationUserId { get; set; } = null!; // Foreign key to ApplicationUser  // التابع يحمل الفورين كي
	}
}