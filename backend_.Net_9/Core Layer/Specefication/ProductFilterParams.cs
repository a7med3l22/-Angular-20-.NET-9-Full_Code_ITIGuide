namespace Core_Layer.Specefication
{
	public class ProductFilterParams
	{
		public string? sort { get; set; }
		public int? categoryId { get; set; }
		public int? brandId { get; set; }
		public string? Search { get; set; }

		private int pageSize=6;
		public int PageSize {
			get { return pageSize; }

			set
			{
				pageSize = value > 0&&value<=10 ? value : 6; // default value is 6, max value is 10
			} 
		}
		private int pageIndex = 1;

		public int PageIndex {
			get { return pageIndex; }

			set
			{
				pageIndex = value > 0 ? value : 1; // default value is 1, min value is 1
			}
		
		}
	}
}