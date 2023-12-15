using Redis.OM.Modeling;

namespace RediSearch_webapi.Models;
[Document(IndexName = "product-idx", StorageType = StorageType.Json, Prefixes = new[] { "Product" })]
public class Product
{
	[RedisIdField]
	public Ulid Id { get; set; }
	[Searchable(Sortable = true)]
	public string Name { get; set; }
	[Indexed(Aggregatable = true)]
	public string Company { get; set; }
	[Indexed(Sortable = true)]
	public double Price { get; set; }
}