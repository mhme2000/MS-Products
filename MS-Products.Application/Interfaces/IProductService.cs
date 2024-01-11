using MS_Products.Domain.Entities;
namespace MS_Products.Application.Interfaces;
public interface IProductService
{
    Task<Product> GetProductById(Guid productId);
    Task<List<Product>> GetProductByCategoryId(Guid categoryId);
    Task<IEnumerable<Product>> GetAllProducts();
    Task DeleteProductById(Guid productId);
    Task UpdateProductById(Product product);
    Task CreateProduct(Product product);
}
