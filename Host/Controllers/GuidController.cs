using System;
using System.Collections.Generic;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Controllers
{
    [Route("guid")]
    public class GuidController : ControllerBase
    {
        private readonly IGuidService _guidService;

        public GuidController(IGuidService guidService)
        {
            _guidService = guidService;
        }

        [HttpGet("get")]
        public IEnumerable<Guid> GetGuidList()
        {
            var middlewareGuid = (Guid) HttpContext.Items["guid"];
            
            var guidList = new List<Guid>()
            {
                _guidService.GetGuid(),
                middlewareGuid
            };

            return guidList;
        }
    }
}