using AutoMapper;
using Core_Layer.Interfaces;
using Core_Layer.Models.Basket;
using Core_Layer.Models.ErrorsHandle;
using Core_Layer.Models.Product;
using Core_Layer.Specefication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Text.Json;
using System.Threading.Tasks;

public class BasketService:IBasketService
{
	private readonly IDatabase db;
	private readonly IApiUrlProvider apiUrl;
	private readonly IMapper mapper;
	private readonly IUnitOfWork unitOfWork;
	private readonly ILogger<BasketService> logger;
	private readonly IConfiguration config;
	private static readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
	{
		PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Use camelCase for property names
	};

	public BasketService(IApiUrlProvider apiUrl,IMapper mapper , IUnitOfWork unitOfWork,IConnectionMultiplexer connection, ILogger<BasketService> logger,IConfiguration config)
	{
		db = connection.GetDatabase();
		this.apiUrl = apiUrl;
		this.mapper = mapper;
		this.unitOfWork = unitOfWork;
		this.logger = logger;
		this.config = config;
	}

	/// Add Or Update a basket to the Redis database
	public async Task<ReturnedBasket> AddOrUpdateBasketAsync(AddBasket basketData) 
	{
		if (basketData is null || string.IsNullOrEmpty(basketData.Id)|| basketData.Items.Count==0)  // ✅ لحماية الطبقة الداخلية في حال تم استخدامها مستقبلًا من Service أو Background Job أو Test.
			throw new ThrowException(400,"Basket Or basket ID Or basket Items Cant be null.");

		/////make BasketItem Price/////
		
		List<ReturnedBasketItem> returnedItems = new();
		foreach (var item in basketData.Items)
		{
			
			var productSpec=new ProductSpecificationsWithoutPagination(item.Id); // Create a new ProductSpecification with the product ID

			var product = await unitOfWork.GetRepo<Product>().GetAsyncAfterSpecId(productSpec); // Get the product details from the repository

			if (product is null||item.Quantity<=0)
			{
				throw new ThrowException(404, $"Product with This ID {item.Id} not found.");
			}
			returnedItems.Add(new ReturnedBasketItem
			{
				Id= item.Id, // Set the product ID
				Quantity = item.Quantity, // Set the product quantity
				ProductName = product.Name, // Set the product name
				Price = product.Price, // Set the product price
				PictureUrl = product.PictureUrl, // Set the product picture URL
				Brand = product.Brand.Name, // Set the product brand
				Category = product.Category.Name // Set the product category
			});
		}

		var returnedBasket=mapper.Map<AddBasket,ReturnedBasket>(basketData); // Map the AddBasket to ReturnedBasket 
		returnedBasket.Items = returnedItems; // Assign the list of returned items to the basket
		if(basketData.DeliveryMethodId!=null)
		{
			var DeliveryMethod = await unitOfWork.GetRepo<DeliveryMethod>().GetAsyncById((basketData.DeliveryMethodId.Value));
			if (DeliveryMethod == null)
				throw new ThrowException(400, $"DeliveryMethodId Can`t Be Null.!");
			returnedBasket.ShippingCost = DeliveryMethod.Cost;
		}

		var basketJson = JsonSerializer.Serialize(returnedBasket, jsonOptions);

		/////////////Redis Life Time/////////////
		int redisLifeInDays;
		var isParsed= int.TryParse(config["Redis:RedisLifeInDays"], out redisLifeInDays); // لو نجح ال بارس خلي ال ريدس لايف تايم بيساوي القيمة اللي نجحت ف البارس
		if(!isParsed || redisLifeInDays <= 0)
		{
			logger.LogWarning("Invalid RedisLifeInDays configuration. Using default value of 5 days.");
			redisLifeInDays = 5; // Default value if parsing fails or is invalid
		}
		var isSaved = await db.StringSetAsync(basketData.Id, basketJson, TimeSpan.FromDays(redisLifeInDays));
		////////////////////////////
		if (isSaved)
		{
			logger.LogInformation("Basket {BasketId} saved/updated successfully.", basketData.Id);
			var basket= await GetBasketAsync(basketData.Id);
			if (basket is null)
			{
				throw new ThrowException(500, $"Basket {basketData.Id} was saved but could not be retrieved.");
			}
			return basket;
		}
		else
		{
			throw new ThrowException(500, $"Failed to save/update basket {basketData.Id}.");
		}
	}

	/// Get a basket From the Redis database
	public async Task<ReturnedBasket?> GetBasketAsync(string id)
	{
		if (string.IsNullOrEmpty(id))
			throw new ThrowException(400,"Basket ID cannot be null or empty.");

		var basketJson = await db.StringGetAsync(id);

		if (basketJson.IsNullOrEmpty)
		{
			logger.LogInformation("Basket {BasketId} not found in Redis.", id);
			return null;
		}

		var basket = JsonSerializer.Deserialize<ReturnedBasket>(basketJson!, jsonOptions)!;
		logger.LogInformation("Basket {BasketId} retrieved successfully.", id);
		foreach (var item in basket.Items)
		{
			if (!string.IsNullOrEmpty(item.PictureUrl) && !item.PictureUrl.StartsWith("http")) //لو الصور أحيانًا محفوظة بـ URL كامل، قد يتكرر 
			{
				item.PictureUrl = apiUrl.GetApiUrl() + item.PictureUrl;// to return the full URL of the product picture
			}
		}
		return basket;
	}

	/// Delete a basket from the Redis database
	public async Task<bool> DeleteBasketAsync(string basketId)
	{
		if (string.IsNullOrEmpty(basketId)) //✅ لحماية الطبقة الداخلية في حال تم استخدامها مستقبلًا من Service أو Background Job أو Test.

			throw new ThrowException(400, "Basket ID cannot be null or empty.");

		var isDeleted = await db.KeyDeleteAsync(basketId);

		if (isDeleted)
			logger.LogInformation("Basket {BasketId} deleted successfully.", basketId);
		else
			logger.LogWarning("Basket {BasketId} was not found for deletion.", basketId);

		return isDeleted;
	}
}
