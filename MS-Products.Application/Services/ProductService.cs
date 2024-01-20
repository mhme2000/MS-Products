using MS_Products.Domain.Entities;
using MS_Products.Domain.Interfaces;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace MS_Products.Application.Services;
public class ProductService
{
    private readonly IProductRepository _repository;
    private readonly IDatabase _cache;
    private readonly ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("elasticache-rg2n57.serverless.use1.cache.amazonaws.com:6379");

    public ProductService(IProductRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _cache = redis.GetDatabase();
    }

    public async Task CreateProduct(Product product)
    {
        await _repository.CreateProduct(product);
        ClearCache();
    }

    public async Task DeleteProductById(Guid productId)
    {
        await _repository.DeleteProductById(productId);
        ClearCache();
    }

    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        var cachedData = await _cache.StringGetAsync("AllProducts");
        if (!cachedData.IsNullOrEmpty)
            return JsonConvert.DeserializeObject<IEnumerable<Product>>(cachedData);
        var products = await _repository.GetAllProducts();
        await _cache.StringSetAsync("AllProducts", JsonConvert.SerializeObject(products));

        return products;
    }

    public async Task<List<Product>> GetProductByCategoryId(Guid categoryId)
    {
        var products = await _repository.GetProductByCategoryId(categoryId);
        return products;
    }

    public async Task<Product> GetProductById(Guid productId)
    {
        var cachedData = await _cache.StringGetAsync($"Product:{productId}");
        if (!cachedData.IsNullOrEmpty)
            return JsonConvert.DeserializeObject<Product>(cachedData);        
        var product = await _repository.GetProductById(productId);
        if (product != null)
        {
            await _cache.StringSetAsync($"Product:{productId}", JsonConvert.SerializeObject(product));
            return product;
        }
        throw new Exception("Produto não encontrado.");
    }

    public async Task UpdateProductById(Product product)
    {
        await GetProductById(product.Id);
        await _repository.UpdateProductById(product);
        ClearCache(product.Id);
    }

    private void ClearCache(Guid? productId = null)
    {
        if(productId != null)_cache.KeyDeleteAsync($"Product:{productId}");
        _cache.KeyDelete("AllProducts");
    }
}
