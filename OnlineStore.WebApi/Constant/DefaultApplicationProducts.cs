using OnlineStore.WebApi.Models;

namespace OnlineStore.WebApi.Constant
{
    public static class DefaultApplicationProducts
    {
        public static List<Product> GetDefaultProducts()
        {
            var products = new List<Product>
            {
                new Product
                {
                     Id =Guid.NewGuid(),
                     ProductName="Samsung New A13",
                     Category=ProductCategory.Mobile,
                     ProductDescription="Mobile Description",
                     ProductPrice=12000,
                     ProductStock=5
                },
                  new Product
                {
                     Id =Guid.NewGuid(),
                     ProductName="Remote Control Car",
                     Category=ProductCategory.Toys,
                     ProductDescription="Remote Control Car Description",
                     ProductPrice=499,
                     ProductStock=10
                },
                   new Product
                {
                     Id =Guid.NewGuid(),
                     ProductName="ASP Net Core Learning",
                     Category=ProductCategory.Books,
                     ProductDescription="Book Description",
                     ProductPrice=299,
                     ProductStock=5
                },
                    new Product
                {
                     Id =Guid.NewGuid(),
                     ProductName="Docker Desktop",
                     Category=ProductCategory.Books,
                     ProductDescription="Docker Desktop Description",
                     ProductPrice=399,
                     ProductStock=8
                },
                    new Product
                {
                     Id =Guid.NewGuid(),
                     ProductName="Moisturizer",
                     Category=ProductCategory.Beauty,
                     ProductDescription="Moisturizer Description",
                     ProductPrice=349,
                     ProductStock=12
                }
            };
            return products;
        }
    }
}
