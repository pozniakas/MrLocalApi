using System;
using System.Collections.Generic;
using System.Xml;

namespace MrLocal_Backend.Repositories.Helpers
{
    public class XmlRepository : EnumConverter
    {
        private readonly ShopRepository shopRepository;
        private readonly ProductRepository productRepository;

        public XmlRepository()
        {
            shopRepository = new ShopRepository();
            productRepository = new ProductRepository();
        }

        public XmlDocument LoadXml(string FileName)
        {
            var doc = new XmlDocument();
            doc.Load(FileName);

            return doc;
        }
        public List<ShopRepository> ReadShopXml(string FileName)
        {
            var doc = LoadXml(FileName);
            var allShops = new List<ShopRepository>();

            foreach (XmlNode nodes in doc.DocumentElement)
            {
                allShops.Add(shopRepository.NodeToObject(nodes));
            }

            return allShops;
        }

        public List<ProductRepository> ReadProductXml(string FileName)
        {
            var doc = LoadXml(FileName);

            var allProducts = new List<ProductRepository>();

            foreach (XmlNode nodes in doc.DocumentElement)
            {
                allProducts.Add(productRepository.NodeToObject(nodes));
            }

            return allProducts;
        }
    }
}
