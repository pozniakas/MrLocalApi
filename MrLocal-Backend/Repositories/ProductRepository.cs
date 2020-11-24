using MrLocal_Backend.Repositories.Helpers;
using MrLocal_Backend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MrLocal_Backend.Repositories
{
    public class ProductRepository : XmlRepository<ProductRepository>, IProductRepository
    {
        readonly string fileName;

        public string Id { get; set; }
        public string ShopId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public PriceTypes PriceType { get; set; }
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
            fileName = ConfigurationManager.AppSettings.Get("PRODUCT_REPOSITORY_FILE_NAME");

            if (!Directory.Exists("Data"))
            {
                Directory.CreateDirectory("Data");
            }

            if (!File.Exists(fileName))
            {
                var xElement = new XElement("Products");
                var xDocument = new XDocument(xElement);
                xDocument.Save(fileName);
            }

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

        public async Task<ProductRepository> Create(string shopId, string name
            , string description, string pricetype, double? price)
        {
            var id = Guid.NewGuid().ToString();
            var doc = await LoadXml(fileName);

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

            doc.Save(fileName);

            return new ProductRepository(id, shopId, name, description, StringToPricetype(pricetype), (double) price);
        }

        public async Task<ProductRepository> Update(string id, string shopId, string name
            , string description, string pricetype, double? price)
        {
            var dateNow = DateTime.Now.ToShortDateString();
            var doc = await LoadXml(fileName);
            var allNodes = doc.SelectNodes("Product");

            foreach (XElement node in allNodes)
            {
                if (node.Element("Id").Value == id && node.Element("ShopId").Value == shopId)
                {
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

                    node.SetElementValue("UpdatedAt", dateNow);
                    doc.Save(fileName);
                }
            }
            return new ProductRepository(id, shopId, name, description, StringToPricetype(pricetype), (double)price);
        }

        public async Task<string> Delete(string id)
        {
            var dateNow = DateTime.Now.ToShortDateString();
            var doc = await LoadXml(fileName);
            var allNodes = doc.SelectNodes("Product");

            foreach (XElement node in allNodes)
            {
                if (node.Element("Id").Value == id)
                {
                    node.SetElementValue("DeletedAt", dateNow);
                    doc.Save(fileName);

                    return id;
                }
            }

            throw new ArgumentException("Can't delete the product with invalid id");
        }

        public async Task<ProductRepository> FindOne(string id)
        {
            var listOfProducts = await ReadXml(fileName);
            return listOfProducts.First(i => i.Id == id && i.DeletedAt == null);
        }

        public async Task<List<ProductRepository>> FindAll(string shopId)
        {
            var listOfProducts = await ReadXml(fileName);
            return listOfProducts.Where(i => i.DeletedAt == null && i.ShopId == shopId).ToList();
        }
    }
}
