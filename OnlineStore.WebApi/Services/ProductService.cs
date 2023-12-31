using OnlineStore.WebApi.Data;
using OnlineStore.WebApi.Models;
using OnlineStore.WebApi.Services.Interfaces;

namespace OnlineStore.WebApi.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _dbContext;
        public ProductService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Product> GetProductList()
        {
            return _dbContext.Products.ToList();
        }
        public Product GetProductById(Guid id)
        {
            return _dbContext.Products.Where(x => x.Id == id).FirstOrDefault();
        }
        public Product CreateProduct(Product product)
        {
            var result = _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
            return result.Entity;
        }
        public Product UpdateProduct(Product product)
        {
            var result = _dbContext.Products.Update(product);
            _dbContext.SaveChanges();
            return result.Entity;
        }
        public bool DeleteProduct(Guid Id)
        {
            var filteredData = _dbContext.Products.Where(x => x.Id == Id).FirstOrDefault();
            var result = _dbContext.Remove(filteredData);
            _dbContext.SaveChanges();
            return result != null ? true : false;
        }
    }
}
