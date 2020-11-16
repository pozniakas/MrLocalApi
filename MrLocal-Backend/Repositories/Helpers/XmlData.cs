using System;
using System.Collections.Generic;
using System.Xml;

namespace MrLocal_Backend.Repositories.Helpers
{
    public class XmlData
    {
        private readonly ConvertPriceType convertPriceType;

        public XmlData()
        {
            convertPriceType = new ConvertPriceType();
        }

        public XmlDocument LoadXml(string FileName)
        {
            var doc = new XmlDocument();
            doc.Load(FileName);

            return doc;
        }

        public List<ProductRepository> ReadProductXml(string FileName)
        {
            var doc = LoadXml(FileName);

            var allProducts = new List<ProductRepository>();

            foreach (XmlNode nodes in doc.DocumentElement)
            {
                allProducts.Add(NodeToProduct(nodes));
            }

            return allProducts;
        }

        public ProductRepository NodeToProduct(XmlNode node)
        {
            var shopId = node["ShopId"].InnerText;
            var id = node["Id"].InnerText;
            var name = node["Name"].InnerText;
            var description = node["Description"].InnerText;
            var price = double.Parse(node["Price"].InnerText);
            var priceType = node["Pricetype"].InnerText;
            var createdAt = node["CreatedAt"].InnerText;
            var updatedAt = node["UpdatedAt"].InnerText;
            var deletedAt = node["DeletedAt"].InnerText;
            var deletedAtValue = deletedAt != "" ? DateTime.Parse(deletedAt) : (DateTime?)null;

            var product = new ProductRepository(id, shopId, name, description, convertPriceType.StringToPricetype(priceType), price)
            {
                UpdatedAt = DateTime.Parse(updatedAt),
                CreatedAt = DateTime.Parse(createdAt),
                DeletedAt = deletedAtValue
            };

            return product;
        }

        public List<ShopRepository> ReadShopXml(string FileName)
        {
            var doc = LoadXml(FileName);
            var allShops = new List<ShopRepository>();

            foreach (XmlNode nodes in doc.DocumentElement)
            {
                allShops.Add(NodeToShop(nodes));
            }

            return allShops;
        }

        public ShopRepository NodeToShop(XmlNode node)
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
