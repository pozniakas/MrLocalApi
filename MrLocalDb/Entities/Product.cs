using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MrLocalDb.Entities
{
    public class Product
    {
        [Key]
        [Column(TypeName = "nvarchar(36)")]
        public string ProductId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(500)")]
        public string Description { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(10)")]
        public PriceTypes PriceType { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        [Required]
        [ForeignKey("ShopId")]
        public string ShopId { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum PriceTypes
        {
            UNIT,
            GRAMS,
            KILOGRAMS
        }

        public Product(string productId, string shopId, string name
    , string description, PriceTypes priceType, decimal price, DateTime createdAt, DateTime updatedAt)
        {
            ProductId = productId;
            ShopId = shopId;
            Name = name;
            Description = description;
            PriceType = priceType;
            Price = price;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            DeletedAt = null;
        }

        public virtual Shop Shop { get; set; }
    }
}
