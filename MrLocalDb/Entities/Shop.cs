﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MrLocalDb.Entities
{
    public class Shop
    {
        [Key]
        [Column(TypeName = "nvarchar(36)")]
        public string ShopId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(10)")]
        public string Status { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(500)")]
        public string Description { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string TypeOfShop { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string City { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual List<Product> Product { get; set; }//lazy loading
    }
}