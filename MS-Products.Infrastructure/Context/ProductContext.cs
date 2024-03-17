using Microsoft.EntityFrameworkCore;
using MS_Products.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace MS_Products.Infrastructure.Context;
public class ProductContext(DbContextOptions<ProductContext> options) : DbContext(options)
{
    public DbSet<Product> Product { get; set; } = null!;
    public DbSet<Category> Category { get; set; } = null!;
}