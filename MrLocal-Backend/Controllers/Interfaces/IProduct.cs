using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MrLocal_Backend.Controllers.Interfaces
{
    interface IProduct
    {
        public Task<Models.Product> Post([FromBody] Models.Product body);
        public Task<Models.Product> Put([FromBody] Models.Product body);
        public Task<string> Delete(string id);
    }
}
