using System.Text.Json.Serialization;

namespace OnlineStore.ViewModel
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int ProductPrice { get; set; }
        public int ProductStock { get; set; }
        public string Category { get; set; }
    }

    public enum ProductCategory
    {
        Electronics,
        Clothing,
        Books,
        Mobile,
        Beauty,
        Travel,
        Toys
    }
}
