using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MrLocalDb.Entities
{
    public class Shop
    {
        public int ShopId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Status { get; set; }
        public string Description { get; set; }
        [Required]
        public string TypeOfShop { get; set; }
        [Required]
        public string City { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }

        public virtual List<Product> Product { get; set; }//lazy loading
    }
}