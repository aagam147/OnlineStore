using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.WebApi.Models;
using OnlineStore.WebApi.Services.Interfaces;
using OnlineStoreMQ.RabbitMQService;

namespace OnlineStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly IRabitMQProducer _rabitMQProducer;
        public ProductController(IProductService productService, IRabitMQProducer _rabitMQProducer) {
            this._rabitMQProducer = _rabitMQProducer;
            this.productService = productService;
        }
        [HttpGet("get-product-list")]
        [AllowAnonymous]
        public IEnumerable<Product> GetProductList()
        {
            var productList = productService.GetProductList();
            return productList;
        }
        [HttpGet("get-product")]
        [AllowAnonymous]
        public Product GetProductById(Guid Id)
        {
            return productService.GetProductById(Id);
        }
        [HttpPost("create-product")]
        [Authorize]
        public Product AddProduct([FromBody]Product product)
        {
            var productData = productService.CreateProduct(product);
            _rabitMQProducer.SendProductMessage(productData);
            return productData;
        }
        [HttpPut("update-product")]
        [Authorize]
        public Product UpdateProduct([FromBody] Product product)
        {
            return productService.UpdateProduct(product);
        }
        [HttpDelete("delete-product")]
        [Authorize]
        public bool DeleteProduct(Guid Id)
        {
            return productService.DeleteProduct(Id);
        }
    }
}
