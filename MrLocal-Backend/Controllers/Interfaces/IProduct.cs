using Microsoft.AspNetCore.Mvc;
using MrLocal_Backend.Repositories;
using System.Threading.Tasks;
using static MrLocal_Backend.Models.Body;

namespace MrLocal_Backend.Controllers.Interfaces
{
    interface IProduct
    {
        Task<ProductRepository> Post([FromBody] ProductBody body);
        Task<ProductRepository> Put([FromBody] ProductBody body);
        Task<string> Delete(string id);
    }
}
