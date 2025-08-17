namespace Core_Layer.Models.Product
{
	public class Product:BaseClass
	{
		public string Name { get; set; } = null!;	
		public string Description { get; set; } = null!;
		public decimal Price { get; set; }
		public string PictureUrl { get; set; } = null!;
		public int CategoryId { get; set; } //  ممكن معملهوش ومكتبش دي HasForeignKey(p=>p.CategoryId) وهو هيعملها اوتوماتيك ويخليها نفس الاسم 
		public int BrandId { get; set; }//  ممكن معملهوش ومكتبش دي HasForeignKey(p=>p.CategoryId) وهو هيعملها اوتوماتيك ويخليها نفس الاسم 
		public virtual Category Category { get; set; }= null!; //has one category with many Product // Take Primary Key Of One As Foreign Key In Many 
		public virtual Brand Brand { get; set; } = null!; // has one brand with many Product // Take Primary Key Of One As Foreign Key In Many 
	}
}
