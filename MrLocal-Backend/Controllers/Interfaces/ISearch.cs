﻿using Microsoft.AspNetCore.Mvc;
using MrLocal_Backend.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MrLocal_Backend.Models.Body;

namespace MrLocal_Backend.Controllers.Interfaces
{
    interface ISearch
    {
        public Task<IActionResult> Get([FromBody] SearchBody body);
    }
}
