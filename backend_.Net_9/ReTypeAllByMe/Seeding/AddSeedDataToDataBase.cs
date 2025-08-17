using Core_Layer.Models.Product;
using Repository_Layer.GenericRepository.Data;
using System.Text.Json;

namespace ReTypeAllByMe.Seeding
{
	public static class AddSeedDataToDataBase
	{
		public async static Task AddSeedData(ApplicationDbContext context)
		{
			//Get Current Path Of Products,DeliveryMethod,Categories and Brands.
			var productsPath = Path.Combine(Directory.GetCurrentDirectory(), "Seeding", "Products.json");
			var deliveryMethodPath = Path.Combine(Directory.GetCurrentDirectory(), "Seeding", "DeliveryMethod.json");
			var categoriesPath = Path.Combine(Directory.GetCurrentDirectory(), "Seeding", "Categories.json");
			var brandsPath = Path.Combine(Directory.GetCurrentDirectory(), "Seeding", "Brands.json");

			//Read All The Json Files 
			var productsData = File.ReadAllText(productsPath);
			var deliveryMethodData = File.ReadAllText(deliveryMethodPath);
			var categoriesData = File.ReadAllText(categoriesPath);
			var brandsData = File.ReadAllText(brandsPath);

			//Deserialize The Json Files To The Models
			/*
			 * ✅ لو بيانات JSON بـ Camel Case أو حالة مختلفة عن كلاس الموديل ➔ استخدم PropertyNameCaseInsensitive = true.
			 * ✅ لو بيانات JSON نفس الكلاس حرفيًا (نفس الحروف الكبيرة والصغيرة) ➔ لا تحتاج JsonSerializerOptions.
			 */
			var products = JsonSerializer.Deserialize<List<Product>>(productsData);
			var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodData);
			var categories = JsonSerializer.Deserialize<List<Category>>(categoriesData);
			var brands = JsonSerializer.Deserialize<List<Brand>>(brandsData);
			//Check If The Data Is Null Or Not
			var doSaveChanges = false;
			if (products != null && products.Count > 0 && !context.Products.Any())
			{
				await context.Products.AddRangeAsync(products);
				doSaveChanges = true;
			}
			if (deliveryMethods != null && deliveryMethods.Count > 0 && !context.DeliveryMethods.Any())
			{
				await context.DeliveryMethods.AddRangeAsync(deliveryMethods);
				doSaveChanges = true;

			}
			if (categories != null && categories.Count > 0 && !context.Categories.Any())
			{
				await context.Categories.AddRangeAsync(categories);
				doSaveChanges = true;

			}
			if (brands != null && brands.Count > 0 && !context.Brands.Any())
			{
				await context.Brands.AddRangeAsync(brands);
				doSaveChanges = true;

			}
			//Save Changes To The Database
			if (doSaveChanges)
			{
				await context.SaveChangesAsync();

			}
		}
	}	
}
