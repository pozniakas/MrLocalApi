﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MrLocal_API.Controllers.Interfaces
{
    interface IProduct
    {
        public Task<IActionResult> Get(string shopId);
        public Task<IActionResult> Post([FromBody] Backend.Models.Product body);
        public Task<IActionResult> Put([FromBody] Backend.Models.Product body);
        public Task<IActionResult> Delete(string id);
    }
}
