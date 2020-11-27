using MrLocal_Backend.Models.Interfaces;
using System;
using System.Text.Json.Serialization;

namespace MrLocal_Backend.Models
{
    public class Product : IModels
    {

        public string Id { get; set; }
        public string ShopId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public PriceTypes PriceType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum PriceTypes
        {
            UNIT,
            GRAMS,
            KILOGRAMS
        }

        public Product() { }

        public Product(string id, string shopId, string name
    , string description, PriceTypes priceType, double price, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            ShopId = shopId;
            Name = name;
            Description = description;
            PriceType = priceType;
            Price = price;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            DeletedAt = null;
        }

    }
}
