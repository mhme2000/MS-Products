using Microsoft.EntityFrameworkCore;
using MS_Products.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace MS_Products.Infrastructure.Context;
[ExcludeFromCodeCoverage]
public class ProductContext : DbContext
{
    public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }
    public DbSet<Product> Product { get; set; } = null!;
    public DbSet<Category> Category { get; set; } = null!;
}