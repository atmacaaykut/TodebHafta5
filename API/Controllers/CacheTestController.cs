using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bussines.Abstract;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheTestController : ControllerBase
    {
        private readonly ICacheExample _cacheExample;

        public CacheTestController(ICacheExample cacheExample)
        {
            _cacheExample = cacheExample;
        }

        [HttpPost]
        public IActionResult Post()
        {
            _cacheExample.DistSetString("TodebTestKey", "TodebTestValue");
            return Ok();
        }

        [HttpPost("SetList")]
        public IActionResult SetList()
        {
            _cacheExample.DistSetList("TodebTestKeyList");
            return Ok();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var list = new List<string>();
            list.Add(_cacheExample.DistGetValue("TodebTestKey"));
            list.Add(_cacheExample.DistGetValue("TodebTestKeyList"));

            return Ok(list);
        }


        [HttpPost("InMemoryTest")]
        public IActionResult InMemoryTest()
        {
            _cacheExample.InMemSetString("InMemoryStr", "InMemoryStrExample");
            _cacheExample.InMemSetObject("InMemoryList",new int[]{1,2,3,6,9});
            return Ok();

        }

        [HttpGet("GetInMemory")]
        public IActionResult GetInMemory()
        {
            var strValue = _cacheExample.InMemGetValue<string>("InMemoryStr");
            var listValue = _cacheExample.InMemGetValue<int[]>("InMemoryList");

            return Ok(new { strValue, listValue });
        }


    }
}
