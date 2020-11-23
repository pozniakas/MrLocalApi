﻿using MrLocal_Backend.Repositories.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace MrLocal_Backend.Repositories
{
    public class ShopRepository : XmlRepository
    {
        readonly string fileName;

        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Status { get; private set; }
        public string Description { get; private set; }
        public string TypeOfShop { get; private set; }
        public string City { get; private set; }
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
                var XmlElement = new XElement("root");
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

        public void Create(string name, string description, string typeOfShop, string city)
        {
            var doc = LoadXml(fileName);

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
            doc.Save(fileName);
        }

        public void Update(string id, string name, string status, string description, string typeOfShop, string city)
        {
            var dateNow = DateTime.Now.ToShortDateString();
            var doc = XDocument.Load(fileName);

            var node = doc.Descendants("Shop").FirstOrDefault(shop => shop.Element("Id").Value == id && shop.Element("DeletedAt").Value == "");

            node.SetElementValue("Name", name);
            node.SetElementValue("Status", status);
            node.SetElementValue("Description", description);
            node.SetElementValue("TypeOfShop", typeOfShop);
            node.SetElementValue("City", city);
            node.SetElementValue("UpdatedAt", dateNow);

            doc.Save(fileName);
        }

        public void Delete(string id)
        {
            var dateNow = DateTime.Now.ToShortDateString();
            var doc = XDocument.Load(fileName);

            var node = doc.Descendants("Shop").FirstOrDefault(cd => cd.Element("Id").Value == id);

            node.SetElementValue("DeletedAt", dateNow);
            node.SetElementValue("Status", "Not Active");

            doc.Save(fileName);
        }

        public ShopRepository FindOne(string id)
        {
            var listOfShop = ReadShopXml(fileName);
            return listOfShop.First(i => i.Id == id && i.DeletedAt == null);
        }

        public List<ShopRepository> FindAll()
        {
            var listOfShop = ReadShopXml(fileName);
            return listOfShop.Where(i => i.DeletedAt == null).ToList();
        }

        public ShopRepository FindOneByName(string name)
        {
            var listOfShop = ReadShopXml(fileName);
            return listOfShop.First(i => i.Name == name && i.DeletedAt == null);
        }
    }
}
