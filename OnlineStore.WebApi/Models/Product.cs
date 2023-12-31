using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnlineStore.WebApi.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int ProductPrice { get; set; }
        public int ProductStock { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ProductCategory Category { get; set; }
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
