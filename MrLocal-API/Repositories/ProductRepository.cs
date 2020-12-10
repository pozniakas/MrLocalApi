using MrLocal_API.Models;
using MrLocal_API.Repositories.Helpers;
using MrLocal_API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MrLocal_API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        readonly string fileName;
        private readonly Lazy<XmlRepository<Product>> xmlRepository = null;
        private readonly Lazy<EnumConverter> enumConverter = null;
        /* private readonly Lazy<IEnumConverter> _enumConverter;

        public ProductRepository(Lazy<IEnumConverter> enumConverter)
        {
            fileName = ConfigurationManager.AppSettings.Get("PRODUCT_REPOSITORY_FILE_NAME");
            xmlRepository = new Lazy<XmlRepository<Product>>();
            _enumConverter = enumConverter ;

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

        }*/
       public ProductRepository()
        {
            fileName = ConfigurationManager.AppSettings.Get("PRODUCT_REPOSITORY_FILE_NAME");
            xmlRepository = new Lazy<XmlRepository<Product>>();
            enumConverter = new Lazy<EnumConverter>();

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

        public async Task<Product> Create(string shopId, string name
            , string description, string pricetype, double? price)
        {
            var id = Guid.NewGuid().ToString();
            var doc = await xmlRepository.Value.LoadXml(fileName);

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

            return new Product(id, shopId, name, description, enumConverter.Value.StringToPricetype(pricetype), (double)price, DateTime.Parse(createdAtStr), DateTime.Parse(updatedAtStr));
        }

        public async Task<Product> Update(string id, string shopId, string name
            , string description, string pricetype, double? price)
        {
            return await Task.Run(() =>
            {
                static bool IsStringEmpty(string str) => str == null || str.Length == 0;

                var dateNow = DateTime.UtcNow.ToString();
                var doc = XDocument.Load(fileName);

                var node = doc.Descendants("Product").FirstOrDefault(product => product.Element("Id").Value == id && product.Element("ShopId").Value == shopId && product.Element("DeletedAt").Value == "");

                string[] titles = { "ShopId", "Name", "Description", "Pricetype", "UpdatedAt" };
                string[] values = { shopId, name, description, pricetype, dateNow };

                for (var i = 0; i < titles.Length; i++)
                {
                    if (!IsStringEmpty(values[i]))
                    {
                        node.SetElementValue(titles[i], values[i]);
                    }
                    else
                    {
                        values[i] = node.Element(titles[i]).Value.ToString();
                    }
                }

                if (price != null)
                {
                    node.SetElementValue("Price", price.ToString());
                }
                else
                {
                    price = double.Parse(node.Element("Price").Value.ToString());
                }

                doc.Save(fileName);

                return new Product(id, values[0], values[1], values[2], enumConverter.Value.StringToPricetype(values[3]), (double)price, DateTime.Parse(node.Element("CreatedAt").Value.ToString()), DateTime.Parse(values[4]));
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

        public async Task<Product> FindOne(string id)
        {
            try
            {
                var listOfProducts = await xmlRepository.Value.ReadXml(fileName);
                return listOfProducts.First(i => i.Id == id && i.DeletedAt == null);
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Product>> FindAll(string shopId)
        {
            var listOfProducts = await xmlRepository.Value.ReadXml(fileName);
            return listOfProducts.Where(i => i.DeletedAt == null && i.ShopId == shopId).ToList();
        }
    }
}
