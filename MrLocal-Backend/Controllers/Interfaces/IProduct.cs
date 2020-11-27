using Microsoft.AspNetCore.Mvc;
using MrLocal_Backend.Models;
using System.Threading.Tasks;
using static MrLocal_Backend.Models.Body;

namespace MrLocal_Backend.Controllers.Interfaces
{
    interface IProduct
    {
        public Task<ProductModel> Post([FromBody] ProductModel body);
        public Task<ProductModel> Put([FromBody] ProductModel body);
        public Task<string> Delete(string id);
    }
}
