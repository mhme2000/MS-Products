using Microsoft.AspNetCore.Mvc;
using MS_Products.Application.Interfaces;
using MS_Products.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace MS_Products.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ExcludeFromCodeCoverage]
    public class ProductController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;
        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(DateTime.Now);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProductAsync([FromBody] Product product)  
        {
            await _productService.CreateProduct(product);
            return Created();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductByIdAsync(Guid id)
        {
            await _productService.DeleteProductById(id);
            return NoContent();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            var retorno = await _productService.GetAllProducts();
            return Ok(retorno.ToList());
        }
        [HttpGet("filterByCategoryId/{categoryId}")]
        public async Task<IActionResult> GetProductByCategoryIdAsync(Guid categoryId)
        {
            var retorno = await _productService.GetProductByCategoryId(categoryId);
            return Ok(retorno);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductByIdAsync(Guid id)
        {
            var retorno = await _productService.GetProductById(id);
            return Ok(retorno);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductByIdAsync(Guid id, [FromBody] Product product)
        {
            product.Id = id;
            await _productService.UpdateProductById(product);
            return NoContent();
        }   
    }
}
