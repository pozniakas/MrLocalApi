using MrLocal.Backend.Models.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;

namespace MrLocal.Backend.Repositories.Interfaces
{
    public interface IXmlRepository<T> where T : IModel
    {
        public Task<XmlDocument> LoadXml(string FileName);
        public Task<List<T>> ReadXml(string FileName);
        public T NodeToObject(XmlNode node);
    }
}
