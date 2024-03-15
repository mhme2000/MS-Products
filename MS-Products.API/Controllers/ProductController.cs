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
        public IActionResult CreateProduct([FromBody] Product product)  
        {
            var retorno = _productService.CreateProduct(product);
            return Ok(retorno);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProductById(Guid id)
        {
            var retorno = _productService.DeleteProductById(id);
            return Ok(retorno);
        }
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var retorno = _productService.GetAllProducts();
            return Ok(retorno);
        }
        [HttpGet("filterByCategoryId/{categoryId}")]
        public IActionResult GetProductByCategoryId(Guid categoryId)
        {
            var retorno = _productService.GetProductByCategoryId(categoryId);
            return Ok(retorno);
        }
        [HttpGet("{id}")]
        public IActionResult GetProductById(Guid id)
        {
            var retorno = _productService.GetProductById(id);
            return Ok(retorno);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateProductById(Guid id, [FromBody] Product product)
        {
            product.Id = id;
            var retorno = _productService.UpdateProductById(product);
            return Ok(retorno);
        }   
    }
}
