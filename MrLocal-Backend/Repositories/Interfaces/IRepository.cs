using System;

namespace MrLocal_Backend.Repositories.Interfaces
{
    interface IRepository
    {
        public string Id { get;  set; }
        public string Name { get;  set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public void Create(string name, string description, string typeOfShop, string city);
        public void Create(string shopId, string name, string description, string pricetype, double? price);
        public void Update(string id, string name, string status, string description, string typeOfShop, string city);
        public void Update(string id, string shopId, string name, string description, string pricetype, double? price);
        public void Delete(string id);     
    }
}
