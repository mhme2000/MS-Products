using MS_Products.Application.Interfaces;
using MS_Products.Domain.Entities;
using MS_Products.Domain.Interfaces;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace MS_Products.Application.Services;
public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IDatabase _cache;
#if !DEBUG
    private readonly ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("elasticache-rg2n57.serverless.use1.cache.amazonaws.com:6379");
#endif
    public ProductService(IProductRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
#if !DEBUG
        _cache = redis.GetDatabase();
#else
        _cache = null;
#endif
    }

    public async Task CreateProduct(Product product)
    {
        await _repository.CreateProduct(product);
        if(_cache != null) ClearCache();
    }

    public async Task DeleteProductById(Guid productId)
    {
        await _repository.DeleteProductById(productId);
        if (_cache != null) ClearCache();
    }

    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        if (_cache != null)
        {
            var cachedData = await _cache.StringGetAsync("AllProducts");
            if (!cachedData.IsNullOrEmpty)
                return JsonConvert.DeserializeObject<IEnumerable<Product>>(cachedData);
        }           
        var products = await _repository.GetAllProducts();
        if (_cache != null) await _cache.StringSetAsync("AllProducts", JsonConvert.SerializeObject(products));

        return products;
    }

    public async Task<List<Product>> GetProductByCategoryId(Guid categoryId)
    {
        var products = await _repository.GetProductByCategoryId(categoryId);
        return products;
    }

    public async Task<Product> GetProductById(Guid productId)
    {
        if (_cache != null) 
        {
            var cachedData = await _cache.StringGetAsync($"Product:{productId}");
            if (!cachedData.IsNullOrEmpty)
                return JsonConvert.DeserializeObject<Product>(cachedData);
        }                  
        var product = await _repository.GetProductById(productId);
        if (product != null)
        {
            if (_cache != null) await _cache.StringSetAsync($"Product:{productId}", JsonConvert.SerializeObject(product));
            return product;
        }
        throw new Exception("Produto não encontrado.");
    }

    public async Task UpdateProductById(Product product)
    {
        await GetProductById(product.Id);
        await _repository.UpdateProductById(product);
        if (_cache != null) ClearCache(product.Id);
    }

    private void ClearCache(Guid? productId = null)
    {
        if(productId != null)_cache.KeyDeleteAsync($"Product:{productId}");
        _cache.KeyDelete("AllProducts");
    }
}
