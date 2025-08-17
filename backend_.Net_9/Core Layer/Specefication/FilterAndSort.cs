namespace Core_Layer.Specefication
{
	public class FilterAndSortProduct
	{
		//sort:name,price,quentity//filter:categoryId,name,category

		public string ? SortBy { get; set; }//
		public int? categoryId { get; set; }//
		public string? name { get; set; }//
		public string? category { get; set; }//
	}
	public class FilterAndSortCategories
	{
		//sort:name,price,quentity//filter:categoryId,name,category

		public string? SortBy { get; set; }//
		public string? category { get; set; }//
	}
}