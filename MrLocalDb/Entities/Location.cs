using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MrLocalDb.Entities
{
    public class Location
    {
        public Location(string locationId, string latitude, string longitude, string shopId, DateTime createdAt, DateTime updatedAt)
        {
            LocationId = locationId;
            Latitude = latitude;
            Longitude = longitude;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            DeletedAt = null;
            ShopId = shopId;
        }

        [Key]
        [Column(TypeName = "nvarchar(36)")]
        public string LocationId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Latitude { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Longitude { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        [Required]
        [ForeignKey("ShopId")]
        public string ShopId { get; set; }
        public virtual Shop Shop { get; set; }
    }
}
