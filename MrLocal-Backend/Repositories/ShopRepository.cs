using MrLocal_Backend.Repositories.Helpers;
using MrLocal_Backend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace MrLocal_Backend.Repositories
{
    public class ShopRepository : XmlRepository, IRepository
    {
        private const string FileName = "Data/Shop.xml";

        public string Id { get;  set; }
        public string Name { get;  set; }
        public string Status { get;  set; }
        public string Description { get;  set; }
        public string TypeOfShop { get;  set; }
        public string City { get;  set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public ShopRepository()
        {
            if (!Directory.Exists("Data"))
            {
                Directory.CreateDirectory("Data");
            }

            if (!File.Exists(FileName))
            {
                var XmlElement = new XElement("root");
                var XmlDocument = new XDocument(XmlElement);
                XmlDocument.Save(FileName);
            }
        }

        public ShopRepository(string id, string name, string status, string description, string typeOfShop, string city, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            Name = name;
            Status = status;
            Description = description;
            TypeOfShop = typeOfShop;
            City = city;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            DeletedAt = null;
        }

        public void Create(string name, string description, string typeOfShop, string city)
        {
            var doc = LoadXml(FileName);

            var shop = doc.CreateElement("Shop");

            var id = Guid.NewGuid().ToString();
            var dateNow = DateTime.Now.ToShortDateString();

            string[] titles = { "Id", "Name", "Status", "Description", "TypeOfShop", "City", "CreatedAt", "UpdatedAt", "DeletedAt" };
            string[] values = { id, name, "Not Active", description, typeOfShop, city, dateNow, dateNow, "" };

            for (var i = 0; i < titles.Length; i++)
            {
                var node = doc.CreateElement(titles[i]);
                node.InnerText = values[i];
                shop.AppendChild(node);
            }

            doc.DocumentElement.AppendChild(shop);
            doc.Save(FileName);
        }

        public void Update(string id, string name, string status, string description, string typeOfShop, string city)
        {
            var dateNow = DateTime.Now.ToShortDateString();
            var doc = XDocument.Load(FileName);

            var node = doc.Descendants("Shop").FirstOrDefault(shop => shop.Element("Id").Value == id && shop.Element("DeletedAt").Value == "");

            node.SetElementValue("Name", name);
            node.SetElementValue("Status", status);
            node.SetElementValue("Description", description);
            node.SetElementValue("TypeOfShop", typeOfShop);
            node.SetElementValue("City", city);
            node.SetElementValue("UpdatedAt", dateNow);

            doc.Save(FileName);
        }

        public void Delete(string id)
        {
            var dateNow = DateTime.Now.ToShortDateString();
            var doc = XDocument.Load(FileName);

            var node = doc.Descendants("Shop").FirstOrDefault(cd => cd.Element("Id").Value == id);

            node.SetElementValue("DeletedAt", dateNow);
            node.SetElementValue("Status", "Not Active");

            doc.Save(FileName);
        }

        public ShopRepository FindOne(string id)
        {
            var listOfShop = ReadShopXml(FileName);
            return listOfShop.First(i => i.Id == id && i.DeletedAt == null);
        }

        public List<ShopRepository> FindAll()
        {
            var listOfShop = ReadShopXml(FileName);
            return listOfShop.Where(i => i.DeletedAt == null).ToList();
        }

        public void Create(string shopId, string name, string description, string pricetype, double? price)
        {
            throw new NotImplementedException();
        }

        public void Update(string id, string shopId, string name, string description, string pricetype, double? price)
        {
            throw new NotImplementedException();
        }
    }
}
