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

        public async Task<ProductRepository> Create(string shopId, string name
            , string description, string pricetype, double? price)
        {
            var id = Guid.NewGuid().ToString();
            var doc = await LoadXml(fileName);

            var updatedAtStr = DateTime.UtcNow.ToString();
            var createdAtStr = DateTime.UtcNow.ToString();
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

            return new ProductRepository(id, shopId, name, description, StringToPricetype(pricetype), (double)price, DateTime.Parse(createdAtStr), DateTime.Parse(updatedAtStr));
        }

        public async Task<ProductRepository> Update(string id, string shopId, string name
            , string description, string pricetype, double? price)
        {
            return await Task.Run(() =>
            {
                var dateNow = DateTime.UtcNow.ToString();
                var doc = XDocument.Load(fileName);

                var node = doc.Descendants("Product").FirstOrDefault(product => product.Element("Id").Value == id && product.Element("ShopId").Value == shopId && product.Element("DeletedAt").Value == "");

                if (name != null && name.Length > 0)
                {
                    node.SetElementValue("Name", name);
                }
                if (description != null && description.Length > 0)
                {
                    node.SetElementValue("Description", description);
                }
                if (pricetype != null && pricetype.Length > 0)
                {
                    node.SetElementValue("Pricetype", pricetype);
                }
                if (price != null)
                {
                    node.SetElementValue("Price", price.ToString());
                }
                node.SetElementValue("UpdatedAt", dateNow);

                doc.Save(fileName);


                return new ProductRepository(id, shopId, (name == null || name.Length == 0) ? node.Element("Name").Value.ToString() : name,
                    (description == null || description.Length == 0) ? node.Element("Description").Value.ToString() : description,
                    (pricetype == null || pricetype.Length == 0) ? StringToPricetype(node.Element("PriceType").Value) : StringToPricetype(pricetype),
                    price == null ? double.Parse(node.Element("Price").Value) : (double)price, DateTime.Parse(node.Element("CreatedAt").Value.ToString()), DateTime.Parse(dateNow));
            });
        }

        public async Task<string> Delete(string id)
        {
            return await Task.Run(() =>
            {
                var doc = XDocument.Load(fileName);

                var node = doc.Descendants("Product").FirstOrDefault(product => product.Element("Id").Value == id);
                node.SetElementValue("DeletedAt", DateTime.UtcNow.ToString());

                doc.Save(fileName);
                return id;
            });
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
