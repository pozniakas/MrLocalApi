using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace MrLocal_Backend.Repositories
{
    public class ShopRepository
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Status { get; private set; }
        public string Description { get; private set; }
        public string TypeOfShop { get; private set; }
        public string City { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public DateTime? DeletedAt { get; private set; }

        public ShopRepository()
        {
            if (!Directory.Exists("Data"))
            {
                Directory.CreateDirectory("Data");
            }

            if (!File.Exists("Data/shops.xml"))
            {
                var XmlElement = new XElement("Shops");
                var XmlDocument = new XDocument(XmlElement);
                XmlDocument.Save("Data/shops.xml");
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
            var doc = LoadShopXml();

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
            doc.Save("Data/shops.xml");
        }

        public void Update(string id, string name, string status, string description, string typeOfShop, string city)
        {
            var dateNow = DateTime.Now.ToShortDateString();
            var doc = XDocument.Load("Data/shops.xml");

            var node = doc.Descendants("Shop").FirstOrDefault(shop => shop.Element("Id").Value == id && shop.Element("DeletedAt").Value == "");

            node.SetElementValue("Name", name);
            node.SetElementValue("Status", status);
            node.SetElementValue("Description", description);
            node.SetElementValue("TypeOfShop", typeOfShop);
            node.SetElementValue("City", city);
            node.SetElementValue("UpdatedAt", dateNow);

            doc.Save("Data/shops.xml");
        }

        public void Delete(string id)
        {
            var dateNow = DateTime.Now.ToShortDateString();
            var doc = XDocument.Load("Data/shops.xml");

            var node = doc.Descendants("Shops").Descendants("Shop").FirstOrDefault(cd => cd.Element("Id").Value == id);

            node.SetElementValue("DeletedAt", dateNow);
            node.SetElementValue("Status", "Not Active");

            doc.Save("Data/shops.xml");
        }

        public ShopRepository FindOne(string id)
        {
            var listOfShop = ReadXml();
            return listOfShop.First(i => i.Id == id && i.DeletedAt == null);
        }

        public List<ShopRepository> FindAll()
        {
            var listOfShop = ReadXml();
            return listOfShop.Where(i => i.DeletedAt == null).ToList();
        }


        private XmlDocument LoadShopXml()
        {
            var doc = new XmlDocument();
            doc.Load("Data/shops.xml");

            return doc;
        }

        private List<ShopRepository> ReadXml()
        {
            var doc = LoadShopXml();
            var allShops = new List<ShopRepository>();

            foreach (XmlNode nodes in doc.DocumentElement)
            {
                allShops.Add(NodeToShop(nodes));
            }

            return allShops;
        }

        private ShopRepository NodeToShop(XmlNode node)
        {
            var _id = node["Id"].InnerText;
            var _name = node["Name"].InnerText;
            var _status = node["Status"].InnerText;
            var _description = node["Description"].InnerText;
            var _typeofShop = node["TypeOfShop"].InnerText;
            var _city = node["City"].InnerText;
            var _createdAt = node["CreatedAt"].InnerText;
            var _updatedAt = node["UpdatedAt"].InnerText;
            var _deletedAt = node["DeletedAt"].InnerText;

            var formattedCreatedAt = DateTime.Parse(_createdAt);
            var formattedUpdatedAt = DateTime.Parse(_updatedAt);
            DateTime? formattedDeletedAt = null;

            if (_deletedAt.Length > 0)
            {
                formattedDeletedAt = DateTime.Parse(_deletedAt);
            }

            var shop = new ShopRepository(_id, _name, _status, _description, _typeofShop, _city, formattedCreatedAt, formattedUpdatedAt)
            {
                DeletedAt = formattedDeletedAt
            };

            return shop;
        }
    }
}
