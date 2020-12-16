using System;
using System.Collections.Generic;

namespace MrLocal.Db.Entities
{
    public class Shop
    {
        public int ShopId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string TypeOfShop { get; set; }
        public string City { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }

        public virtual List<Product> Product { get; set; }//lazy loading
    }
}
