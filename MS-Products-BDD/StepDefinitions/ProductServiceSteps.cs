using Moq;
using MS_Products.Application.Services;
using MS_Products.Domain.Entities;
using MS_Products.Domain.Interfaces;
using TechTalk.SpecFlow;
using Xunit;
namespace StepDefinitions;
[Binding]
public class ProductServiceSteps
{
    private readonly ProductService _productService;
    private Guid _productId;
    private string _productName;
    private Exception _exception;

    public ProductServiceSteps()
    {
        _productService = new ProductService(new Mock<IProductRepository>().Object);
    }

    [Given(@"a product with ID ""(.*)"" and name ""(.*)""")]
    public void GivenAProductWithIDAndName(Guid productId, string productName)
    {
        _productId = productId;
        _productName = productName;
    }

    [When(@"I create the product")]
    public async Task WhenICreateTheProduct()
    {
        try
        {
            var product = new Product(_productName, 10.70M, Guid.NewGuid());
            await _productService.CreateProduct(product);
        }
        catch (Exception ex)
        {
            _exception = ex;
        }
    }

    [Then(@"the product should be created successfully")]
    public void ThenTheProductShouldBeCreatedSuccessfully()
    {
        Assert.Null(_exception);
    }

    [When(@"I update the product name to ""(.*)""")]
    public async Task WhenIUpdateTheProductNameTo(string updatedProductName)
    {
        try
        {
            var product = new Product(updatedProductName, 10.70M, Guid.NewGuid());
            product.Id = _productId;
            await _productService.UpdateProductById(product);
        }
        catch (Exception ex)
        {
            _exception = ex;
        }
    }

    [Then(@"the product should be updated successfully")]
    public void ThenTheProductShouldBeUpdatedSuccessfully()
    {
        Assert.Null(_exception);
    }
}
