using Core_Layer.Models.Product;

namespace ReTypeAllByMe.Dto
{
	public class ReturnedResponse
	{
		public int PageIndex { get; set; }
		public int PageSize { get; set; }
		public int Count { get; set; }
		public IReadOnlyList<ProductDto> Data { get; set; } = null!;
	}
}
