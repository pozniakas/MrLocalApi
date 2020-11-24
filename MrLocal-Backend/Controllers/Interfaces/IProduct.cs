using Microsoft.AspNetCore.Mvc;
using static MrLocal_Backend.Models.Body;

namespace MrLocal_Backend.Controllers.Interfaces
{
    interface IProduct
    {
        public string Post([FromBody] ProductBody body);
        public string Put([FromBody] ProductBody body);
        public string Delete(string id);
    }
}
