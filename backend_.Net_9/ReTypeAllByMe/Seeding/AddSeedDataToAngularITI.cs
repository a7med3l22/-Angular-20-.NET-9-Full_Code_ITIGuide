using Core_Layer.Models.Product;
using Repository_Layer.AngularITI;
using Repository_Layer.GenericRepository.Data;
using System.Text.Json;

namespace ReTypeAllByMe.Seeding
{
	public static class AddSeedDataToAngularITI
	{
		public async static Task AddSeedData(AngularDBContext context)
		{
			// عاوز اقرأ ملف ال جيسون بتاع ITIProducts

			// عاوز اجيب المسار اللي فيه الملف الاول 
			var pathOfITIProductsJsonFile = Path.Combine(Directory.GetCurrentDirectory(), "Seeding", "ITIProducts.json");
			var pathOfCategoriesJsonFile = Path.Combine(Directory.GetCurrentDirectory(), "Seeding", "Categories.json");


			var ProductsJsonFile = File.ReadAllText(pathOfITIProductsJsonFile);
			var CategoriesJsonFile = File.ReadAllText(pathOfCategoriesJsonFile);

			// عاوز احول ملف الجيسون للكلاس 
			var angularProducts = JsonSerializer.Deserialize<List<angularProducts>>(ProductsJsonFile);
			var categoryProducts = JsonSerializer.Deserialize<List<Category>>(CategoriesJsonFile);

			bool isSave = false;
			if (angularProducts != null &&!context.angularProducts.Any())
			{
				await context.angularProducts.AddRangeAsync(angularProducts);
				isSave = true;
			}
			if (categoryProducts != null && !context.Categories.Any())
			{
				await context.Categories.AddRangeAsync(categoryProducts);
				isSave = true;
			}
			if(isSave)
			{
				await context.SaveChangesAsync();
			}
		}

	}
}
