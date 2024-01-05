using Bogus;
using Redis.OM;
using RediSearch_webapi.Factories;
using RediSearch_webapi.Models;

namespace RediSearch_webapi.Services;

public class ProductService
{
	private readonly RedisConnectionFactory _redisConnectionFactory;
	private readonly Faker _faker;
	public ProductService(RedisConnectionFactory redisConnectionFactory)
	{
		_redisConnectionFactory = redisConnectionFactory;
		_faker = new Faker();
	}
	public async Task CreateProductIndex(CancellationToken cancellationToken)
	{
		var provider = new RedisConnectionProvider(_redisConnectionFactory.GetWriteConnection());
		await provider.Connection.CreateIndexAsync(typeof(Product));
	}

	public async Task<Product> AddRandomProduct(CancellationToken cancellationToken)
	{
		var provider = new RedisConnectionProvider(_redisConnectionFactory.GetWriteConnection());
		var productCollection = provider.RedisCollection<Product>();

		var product = new Product
		{
			Company = _faker.Vehicle.Manufacturer(),
			Name = _faker.Vehicle.Model(),
		
			Price =(double)_faker.Random.Number(100, 200000000)
		};
		await productCollection.InsertAsync(product);
		return product;
	}
	public async Task AddRandomProducts(int count,CancellationToken cancellationToken)
	{
		var provider = new RedisConnectionProvider(_redisConnectionFactory.GetWriteConnection());
		var productCollection = provider.RedisCollection<Product>();
		var products = new List<Product>();
		for (int i = 0; i < count; i++)
		{
			products.Add( new Product
			{
				Company = _faker.Vehicle.Manufacturer(),
				Name = _faker.Vehicle.Model(),

				Price = (double)_faker.Random.Number(100, 200000000)
			});
		}
		
		await productCollection.InsertAsync(products);
	}

	public  Task<IList<Product>> GetAll(CancellationToken cancellationToken)
	{
		var provider = new RedisConnectionProvider(_redisConnectionFactory.GetReadConnection());
		var productCollection = provider.RedisCollection<Product>();
		return productCollection.Skip(0).Take(100).ToListAsync();
	}
}