using Moq;
using MS_Products.Application.Services;
using MS_Products.Domain.Entities;
using MS_Products.Domain.Interfaces;
using Xunit;
namespace MS_Products.Tests;
public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _repositoryMock;
    private readonly ProductService _productService;

    public ProductServiceTests()
    {
        _repositoryMock = new Mock<IProductRepository>();
        _productService = new ProductService(_repositoryMock.Object);
    }

    [Fact]
    [Trait("ProductService", "CreateProduct")]
    public async Task CreateProduct_ShouldCallRepositoryCreateProduct()
    {
        // Arrange
        var product = new Product("test", 10.12M, Guid.NewGuid());

        // Act
        await _productService.CreateProduct(product);

        // Assert
        _repositoryMock.Verify(repo => repo.CreateProduct(product), Times.Once);
    }

    [Fact]
    [Trait("ProductService", "DeleteProductById")]
    public async Task DeleteProductById_ShouldCallRepositoryDeleteProductById()
    {
        // Arrange
        var productId = Guid.NewGuid();

        // Act
        await _productService.DeleteProductById(productId);

        // Assert
        _repositoryMock.Verify(repo => repo.DeleteProductById(productId), Times.Once);
    }

    [Fact]
    [Trait("ProductService", "GetAllProducts")]
    public async Task GetAllProducts_ShouldCallRepositoryGetAllProducts()
    {
        // Act
        await _productService.GetAllProducts();

        // Assert
        _repositoryMock.Verify(repo => repo.GetAllProducts(), Times.Once);
    }

    [Fact]
    [Trait("ProductService", "GetProductByCategoryId")]
    public async Task GetProductByCategoryId_ShouldCallRepositoryGetProductByCategoryId()
    {
        // Arrange
        var categoryId = Guid.NewGuid();

        // Act
        await _productService.GetProductByCategoryId(categoryId);

        // Assert
        _repositoryMock.Verify(repo => repo.GetProductByCategoryId(categoryId), Times.Once);
    }

    [Fact]
    [Trait("ProductService", "GetProductById")]
    public async Task GetProductById_ShouldCallRepositoryGetProductById()
    {
        // Arrange
        var productId = Guid.NewGuid();
        _repositoryMock.Setup(repo => repo.GetProductById(productId)).ReturnsAsync(new Product("test", 10.12M, Guid.NewGuid()));

        // Act
        await _productService.GetProductById(productId);

        // Assert
        _repositoryMock.Verify(repo => repo.GetProductById(productId), Times.Once);
    }

    [Fact]
    [Trait("ProductService", "GetProductById")]
    public async Task GetProductById_ShouldThrowException_WhenProductNotFound()
    {
        // Arrange
        var productId = Guid.NewGuid();
        _repositoryMock.Setup(repo => repo.GetProductById(productId)).ReturnsAsync((Product)null);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _productService.GetProductById(productId));
    }

    [Fact]
    [Trait("ProductService", "UpdateProductById")]
    public async Task UpdateProductById_ShouldCallGetProductByIdAndRepositoryUpdateProductById()
    {
        // Arrange
        var product = new Product("test", 10.12M, Guid.NewGuid());
        _repositoryMock.Setup(repo => repo.GetProductById(product.Id)).ReturnsAsync(product);

        // Act
        await _productService.UpdateProductById(product);

        // Assert
        _repositoryMock.Verify(repo => repo.GetProductById(product.Id), Times.Once);
        _repositoryMock.Verify(repo => repo.UpdateProductById(product), Times.Once);
    }
}