using MS_Products.Application.Interfaces;
using MS_Products.Domain.Entities;
using MS_Products.Domain.Interfaces;
namespace MS_Products.Application.Services;
public class ProductService(IProductRepository repository) : IProductService
{
    private readonly IProductRepository _repository = repository;
    public Task CreateProduct(Product product)
    {
        return _repository.CreateProduct(product);
    }

    public Task DeleteProductById(Guid productId)
    {
        return _repository.DeleteProductById(productId);
    }

    public Task<IEnumerable<Product>> GetAllProducts()
    {
        return _repository.GetAllProducts();
    }

    public Task<List<Product>> GetProductByCategoryId(Guid categoryId)
    {
        return _repository.GetProductByCategoryId(categoryId);
    }

    public async Task<Product> GetProductById(Guid productId)
    {
        var info = await _repository.GetProductById(productId);
        return info ?? throw new Exception("Produto não encontrado.");
    }

    public async Task UpdateProductById(Product product)
    {
        await GetProductById(product.Id);      
        await _repository.UpdateProductById(product);
    }
}
