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
    public class ShopRepository : IShopRepository
    {
        readonly string fileName;
        private readonly Lazy<XmlRepository<ShopRepository>> xmlRepository = null;

        public string Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string TypeOfShop { get; set; }
        public string City { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public ShopRepository()
        {
            fileName = ConfigurationManager.AppSettings.Get("SHOP_REPOSITORY_FILE_NAME");
            xmlRepository = new Lazy<XmlRepository<ShopRepository>>();

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

        public async Task<ShopRepository> Create(string name, string description, string typeOfShop, string city)
        {
            var doc = await xmlRepository.Value.LoadXml(fileName);

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

            return new ShopRepository(id, name, "Not Active", description, typeOfShop, city, DateTime.Parse(dateNow), DateTime.Parse(dateNow));
        }

        public async Task<ShopRepository> Update(string id, string name, string status, string description, string typeOfShop, string city)
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

                return new ShopRepository(id, values[0], values[1], values[2], values[3], values[4], DateTime.Parse(node.Element("CreatedAt").Value.ToString()), DateTime.Parse(values[5]));
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

        public async Task<ShopRepository> FindOne(string id)
        {
            var listOfShop = await xmlRepository.Value.ReadXml(fileName);
            return listOfShop.First(i => i.Id == id && i.DeletedAt == null);
        }

        public async Task<List<ShopRepository>> FindAll()
        {
            var listOfShop = await xmlRepository.Value.ReadXml(fileName);
            return listOfShop.Where(i => i.DeletedAt == null).ToList();
        }
    }
}
