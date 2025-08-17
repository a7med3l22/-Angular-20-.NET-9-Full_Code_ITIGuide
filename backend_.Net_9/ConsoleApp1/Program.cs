public class Program
{
	public async static Task Main(string[] args)
	{
		var ccc = new ss();
		ccc.x();
	}
}

public class ProductDto
{
	
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public string Description { get; set; } = null!;
	public decimal Price { get; set; }
	public string PictureUrl { get; set; } = null!;
	public string Category { get; set; } = null!;
	public int CategoryId { get; set; }
	public int BrandId { get; set; }
	public string ProductBrand { get; set; } = null!;
}
public class ss
{
	public ProductDto ProductDto { get; set; } = new ProductDto() { BrandId = 7 };

	public void x()
	{
		ProductDto = new()
		{
			BrandId = 8
		};
	}
}