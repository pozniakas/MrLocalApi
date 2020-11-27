﻿using MrLocal_Backend.Models;
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
    public class ProductRepository : IProductRepository
    {
        readonly string fileName;
        private readonly Lazy<XmlRepository<ProductModel>> xmlRepository = null;
        private readonly Lazy<EnumConverter> enumConverter = null;

        public ProductRepository()
        {
            fileName = ConfigurationManager.AppSettings.Get("PRODUCT_REPOSITORY_FILE_NAME");
            xmlRepository = new Lazy<XmlRepository<ProductModel>>();
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

        public async Task<ProductModel> Create(string shopId, string name
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

            return new ProductModel(id, shopId, name, description, enumConverter.Value.StringToPricetype(pricetype), (double)price, DateTime.Parse(createdAtStr), DateTime.Parse(updatedAtStr));
        }

        public async Task<ProductModel> Update(string id, string shopId, string name
            , string description, string pricetype, double? price)
        {
            return await Task.Run(() =>
            {
                static bool IsStringEmpty(string str) => str == null || str.Length == 0;

                var dateNow = DateTime.UtcNow.ToString();
                var doc = XDocument.Load(fileName);

                var node = doc.Descendants("Product").FirstOrDefault(product => product.Element("Id").Value == id && product.Element("ShopId").Value == shopId && product.Element("DeletedAt").Value == "");

                string[] titles = { "Id", "ShopId", "Name", "Description", "Pricetype", "UpdatedAt" };
                string[] values = { id, shopId, name, description, pricetype, dateNow };

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

                return new ProductModel(values[0], values[1], values[2], values[3], enumConverter.Value.StringToPricetype(values[4]), (double)price, DateTime.Parse(node.Element("CreatedAt").Value.ToString()), DateTime.Parse(values[5]));
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

        public async Task<ProductModel> FindOne(string id)
        {
            var listOfProducts = await xmlRepository.Value.ReadXml(fileName);
            return listOfProducts.First(i => i.Id == id && i.DeletedAt == null);
        }

        public async Task<List<ProductModel>> FindAll(string shopId)
        {
            var listOfProducts = await xmlRepository.Value.ReadXml(fileName);
            return listOfProducts.Where(i => i.DeletedAt == null && i.ShopId == shopId).ToList();
        }
    }
}
