using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MrLocal_Backend.Models.Body;

namespace MrLocal_Backend.Controllers.Interfaces
{
    interface ISearch
    {
        public Task<List<Models.Shop>> Get([FromBody] SearchBody body);
    }
}
