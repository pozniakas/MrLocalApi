using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;

namespace MrLocal_Backend.Repositories.Helpers
{
    public class XmlRepository<T> : EnumConverter
    {
        public async Task<XmlDocument> LoadXml(string FileName)
        {
            return await Task.Run(() =>
             {
                 var doc = new XmlDocument();
                 doc.Load(FileName);

                 return doc;
             });
        }

        public async Task<List<T>> ReadXml(string FileName)
        {
            var doc = await LoadXml(FileName);
            var allObjects = new List<T>();

            foreach (XmlNode nodes in doc.DocumentElement)
            {
                allObjects.Add(NodeToObject(nodes));
            }

            return allObjects;
        }

        public T NodeToObject(XmlNode node)
        {
            var _id = node["Id"].InnerText;
            var _name = node["Name"].InnerText;
            var _description = node["Description"].InnerText;
            var _createdAt = node["CreatedAt"].InnerText;
            var _updatedAt = node["UpdatedAt"].InnerText;
            var _deletedAt = node["DeletedAt"].InnerText;

            var formattedCreatedAt = DateTime.Parse(_createdAt);
            var formattedUpdatedAt = DateTime.Parse(_updatedAt);
            var formattedDeletedAt = _deletedAt != "" ? DateTime.Parse(_deletedAt) : (DateTime?)null;

            if (typeof(T) == typeof(ShopRepository))
            {
                var _city = node["City"].InnerText;
                var _status = node["Status"].InnerText;
                var _typeofShop = node["TypeOfShop"].InnerText;

                var shop = new ShopRepository(_id, _name, _status, _description, _typeofShop, _city, formattedCreatedAt, formattedUpdatedAt)
                {
                    DeletedAt = formattedDeletedAt
                };

                return (T)(object)shop;
            }

            else if (typeof(T) == typeof(ProductRepository))
            {
                var price = double.Parse(node["Price"].InnerText);
                var priceType = node["Pricetype"].InnerText;
                var shopId = node["ShopId"].InnerText;

                var product = new ProductRepository(_id, shopId, _name, _description, StringToPricetype(priceType), price, formattedCreatedAt, formattedUpdatedAt)
                {
                    DeletedAt = formattedDeletedAt
                };

                return (T)(object)product;
            }
            else
            {
                throw new TypeAccessException();
            }
        }
    }
}
