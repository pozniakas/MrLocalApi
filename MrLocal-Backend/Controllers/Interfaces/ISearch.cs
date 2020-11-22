using Microsoft.AspNetCore.Mvc;
using MrLocal_Backend.Repositories;
using System.Collections.Generic;
using static MrLocal_Backend.Models.Body;

namespace MrLocal_Backend.Controllers.Interfaces
{
    interface ISearch
    {
        public List<ShopRepository> Get([FromBody] SearchBody body);
    }
}
