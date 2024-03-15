using MS_Products.Application.Interfaces;
using MS_Products.Domain.Entities;
using MS_Products.Domain.Interfaces;

namespace MS_Products.Application.Services;
public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    public ProductService(IProductRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task CreateProduct(Product product)
    {
        await _repository.CreateProduct(product);
    }

    public async Task DeleteProductById(Guid productId)
    {
        await _repository.DeleteProductById(productId);
    }

    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        var products = await _repository.GetAllProducts();
        return products;
    }

    public async Task<List<Product>> GetProductByCategoryId(Guid categoryId)
    {
        var products = await _repository.GetProductByCategoryId(categoryId);
        return products;
    }

    public async Task<Product> GetProductById(Guid productId)
    {
        var product = await _repository.GetProductById(productId);
        if (product != null)
        {
            return product;
        }
        throw new Exception("Produto não encontrado.");
    }

    public async Task UpdateProductById(Product product)
    {
        await GetProductById(product.Id);
        await _repository.UpdateProductById(product);
    }
}
