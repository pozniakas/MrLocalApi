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
    public class ShopRepository : XmlRepository<ShopRepository>, IShopRepository
    {
        readonly string fileName;

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
            var doc = await LoadXml(fileName);

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
                var dateNow = DateTime.UtcNow.ToString();
                var doc = XDocument.Load(fileName);

                var node = doc.Descendants("Shop").FirstOrDefault(shop => shop.Element("Id").Value == id && shop.Element("DeletedAt").Value == "");

                if (name != null && name.Length > 0)
                {
                    node.SetElementValue("Name", name);
                }
                if (status != null && status.Length > 0)
                {
                    node.SetElementValue("Status", status);
                }
                if (description != null && description.Length > 0)
                {
                    node.SetElementValue("Description", description);
                }
                if (typeOfShop != null && typeOfShop.Length > 0)
                {
                    node.SetElementValue("TypeOfShop", typeOfShop);
                }
                if (city != null && city.Length > 0)
                {
                    node.SetElementValue("City", city);
                }
                node.SetElementValue("UpdatedAt", dateNow);

                doc.Save(fileName);

                return new ShopRepository(id, (name == null || name.Length == 0) ? node.Element("Name").Value.ToString() : name,
                    (status == null || status.Length == 0) ? node.Element("Status").Value.ToString() : status,
                    (description == null || description.Length == 0) ? node.Element("Description").Value.ToString() : description,
                    (typeOfShop == null || typeOfShop.Length == 0) ? node.Element("TypeOfShop").Value.ToString() : typeOfShop,
                    (city == null || city.Length == 0) ? node.Element("City").Value.ToString() : city, DateTime.Parse(node.Element("CreatedAt").Value.ToString()), DateTime.Parse(dateNow));
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
            var listOfShop = await ReadXml(fileName);
            return listOfShop.First(i => i.Id == id && i.DeletedAt == null);
        }

        public async Task<List<ShopRepository>> FindAll()
        {
            var listOfShop = await ReadXml(fileName);
            return listOfShop.Where(i => i.DeletedAt == null).ToList();
        }
    }
}
