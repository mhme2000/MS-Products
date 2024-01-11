using Microsoft.EntityFrameworkCore;
using MS_Products.Domain.Entities;
using MS_Products.Domain.Interfaces;
using MS_Products.Infrastructure.Context;

namespace MS_Products.Infrastructure.Repositories
{
    public class ProductRepository(ProductContext context) : IProductRepository
    {
        private readonly ProductContext _context = context;
        public async Task CreateProduct(Product product)
        {
            await _context.Product.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductById(Guid productId)
        {
            await _context.Product.Where(t => t.Id == productId).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _context.Product.AsNoTracking().ToListAsync();       
        }

        public async Task<List<Product>> GetProductByCategoryId(Guid categoryId)
        {
            return await _context.Product.AsNoTracking().Where(t => t.CategoryId == categoryId).ToListAsync();
        }

        public async Task<Product> GetProductById(Guid productId)
        {
            return await _context.Product.FirstOrDefaultAsync(t => t.Id == productId);
        }

        public async Task UpdateProductById(Product product)
        {
            await Task.Run(() => _context.Product.Update(product));
            await _context.SaveChangesAsync();
        }
    }
}
