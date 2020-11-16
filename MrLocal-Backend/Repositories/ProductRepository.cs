using MrLocal_Backend.Repositories.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace MrLocal_Backend.Repositories
{
    public class ProductRepository
    {
        private const string FileName = "Data/Product.xml";
        private readonly XmlData xmlData;

        public string Id { get; private set; }
        public string ShopId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public double Price { get; private set; }
        public PriceTypes PriceType { get; private set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public enum PriceTypes
        {
            UNIT,
            GRAMS,
            KILOGRAMS
        }
        public ProductRepository()
        {
            if (!Directory.Exists("Data"))
            {
                Directory.CreateDirectory("Data");
            }

            if (!File.Exists(FileName))
            {
                var xElement = new XElement("root");
                var xDocument = new XDocument(xElement);
                xDocument.Save(FileName);
            }

            xmlData = new XmlData();
        }

        public ProductRepository(string id, string shopId, string name
            , string description, PriceTypes priceType, double price)
        {
            Id = id;
            ShopId = shopId;
            Name = name;
            Description = description;
            PriceType = priceType;
            Price = price;
        }

        public void Create(string shopId, string name
            , string description, string pricetype, double? price)
        {
            var id = Guid.NewGuid().ToString();
            var doc = xmlData.LoadXml(FileName);

            var updatedAtStr = DateTime.Now.ToShortDateString();
            var createdAtStr = DateTime.Now.ToShortDateString();
            var deletedAtStr = "";

            var product = doc.CreateElement("Product");

            string[] titles = { "Id", "ShopId", "Name", "Description", "Pricetype", "Price", "CreatedAt", "UpdatedAt", "DeletedAt" };
            string[] values = { id, shopId, name, description, pricetype, price?.ToString("#.##"), createdAtStr, updatedAtStr, deletedAtStr };

            for (var i = 0; i < titles.Length; i++)
            {
                var node = doc.CreateElement(titles[i]);
                node.InnerText = values[i];
                product.AppendChild(node);
            }

            doc.DocumentElement.AppendChild(product);

            doc.Save(FileName);
        }

        public void Update(string id, string shopId, string name
            , string description, string pricetype, double? price)
        {
            var doc = XDocument.Load(FileName);

            var node = doc.Descendants("Product").FirstOrDefault(product => product.Element("Id").Value == id && product.Element("ShopId").Value == shopId
            && product.Element("DeletedAt").Value == "");

            if (name != null)
            {
                node.SetElementValue("Name", name);
            }
            if (description != null)
            {
                node.SetElementValue("Description", description);
            }
            if (pricetype != null)
            {
                node.SetElementValue("Pricetype", pricetype);
            }
            if (price != null)
            {
                node.SetElementValue("Price", price.ToString());
            }

            doc.Save(FileName);
        }

        public void Delete(string id)
        {
            var doc = XDocument.Load(FileName);

            var node = doc.Descendants("Product").FirstOrDefault(product => product.Element("Id").Value == id);
            node.SetElementValue("DeletedAt", DateTime.Now.ToShortDateString());

            doc.Save(FileName);
        }

        public ProductRepository FindOne(string id)
        {
            var listOfProducts = xmlData.ReadProductXml(FileName);
            return listOfProducts.First(i => i.Id == id && i.DeletedAt == null);
        }

        public List<ProductRepository> FindAll(string shopId)
        {
            var listOfProducts = xmlData.ReadProductXml(FileName);
            return listOfProducts.Where(i => i.DeletedAt == null && i.ShopId == shopId).ToList();
        }




    }
}