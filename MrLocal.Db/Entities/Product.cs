using System;

namespace MrLocal.Db.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ShopId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PriceType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }

        public virtual Shop Shop { get; set; }//lazy loading
    }
}
