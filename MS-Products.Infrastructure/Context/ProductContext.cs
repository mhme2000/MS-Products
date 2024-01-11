using Microsoft.EntityFrameworkCore;
using MS_Products.Domain.Entities;

namespace MS_Products.Infrastructure.Context;

public class ProductContext : DbContext
{
    public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }
    public DbSet<Product> Product { get; set; } = null!;
    public DbSet<Category> Category { get; set; } = null!;
}