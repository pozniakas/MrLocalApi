using Backend.Models;
using Backend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Backend.Repositories
{
    public class ShopRepository : IShopRepository
    {
        readonly string fileName;
        private readonly Lazy<IXmlRepository<Shop>> _xmlRepository = null;

        public ShopRepository(Lazy<IXmlRepository<Shop>> xmlRepository)
        {
            fileName = ConfigurationManager.AppSettings.Get("SHOP_REPOSITORY_FILE_NAME");
            _xmlRepository = xmlRepository;

            if (!Directory.Exists("Data"))
            {
                Directory.CreateDirectory("Data");
            }

            if (!File.Exists(fileName))
            {
                var XmlElement = new XElement("Shops");
                var XmlDocument = new XDocument(XmlElement);
                XmlDocument.Save(fileName);
            }
        }

        public async Task<Shop> Create(string name, string description, string typeOfShop, string city)
        {
            var doc = await _xmlRepository.Value.LoadXml(fileName);

            var shop = doc.CreateElement("Shop");

            var id = Guid.NewGuid().ToString();
            var dateNow = DateTime.UtcNow.ToString();

            string[] titles = { "Id", "Name", "Status", "Description", "TypeOfShop", "City", "CreatedAt", "UpdatedAt", "DeletedAt" };
            string[] values = { id, name, "Not Active", description, typeOfShop, city, dateNow, dateNow, "" };

            for (var i = 0; i < titles.Length; i++)
            {
                var node = doc.CreateElement(titles[i]);
                node.InnerText = values[i];
                shop.AppendChild(node);
            }

            doc.DocumentElement.AppendChild(shop);
            doc.Save(fileName);

            return new Shop(id, name, "Not Active", description, typeOfShop, city, DateTime.Parse(dateNow), DateTime.Parse(dateNow));
        }

        public async Task<Shop> Update(string id, string name, string status, string description, string typeOfShop, string city)
        {
            return await Task.Run(() =>
            {
                static bool IsStringEmpty(string str) => str == null || str.Length == 0;

                var dateNow = DateTime.UtcNow.ToString();
                var doc = XDocument.Load(fileName);

                var node = doc.Descendants("Shop").FirstOrDefault(shop => shop.Element("Id").Value == id && shop.Element("DeletedAt").Value == "");

                string[] titles = { "Name", "Status", "Description", "TypeOfShop", "City", "UpdatedAt" };
                string[] values = { name, status, description, typeOfShop, city, dateNow };

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

                doc.Save(fileName);

                return new Shop(id, values[0], values[1], values[2], values[3], values[4], DateTime.Parse(node.Element("CreatedAt").Value.ToString()), DateTime.Parse(values[5]));
            });
        }

        public async Task<string> Delete(string id)
        {
            return await Task.Run(() =>
            {
                var dateNow = DateTime.UtcNow.ToString();
                var doc = XDocument.Load(fileName);

                var node = doc.Descendants("Shop").FirstOrDefault(cd => cd.Element("Id").Value == id);

                node.SetElementValue("DeletedAt", dateNow);
                node.SetElementValue("Status", "Not Active");

                doc.Save(fileName);
                return id;
            });
        }

        public async Task<Shop> FindOne(string id)
        {
            try
            {
                var listOfShop = await _xmlRepository.Value.ReadXml(fileName);
                return listOfShop.First(i => i.Id == id && i.DeletedAt == null);
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Shop>> FindAll()
        {
            var listOfShop = await _xmlRepository.Value.ReadXml(fileName);
            return listOfShop.Where(i => i.DeletedAt == null).ToList();
        }
    }
}
