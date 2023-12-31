using OnlineStore.WebApi.Models;

namespace OnlineStore.WebApi.Services.Interfaces
{
    public interface IProductService
    {
        public IEnumerable<Product> GetProductList();
        public Product GetProductById(Guid id);
        public Product CreateProduct(Product product);
        public Product UpdateProduct(Product product);
        public bool DeleteProduct(Guid Id);
    }
}
