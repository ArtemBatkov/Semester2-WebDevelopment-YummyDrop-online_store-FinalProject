using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;
using YummyDrop_online_store.Models;
using YummyDrop_online_store.Services.GeneratorService;
using System.Diagnostics;


namespace YummyDrop_online_store.Controllers
{
    [ApiController]
    [Route("api/yummy")]
    public class YummyAPIController : ControllerBase
    {
       
        private readonly IGeneratorService _generator;
        private static YummyItem? _lastObject1 = 
            _lastObject1 = new YummyItem { Name = "Default" };

        private static bool called = false;

    private YummyAPIController? yummyAPIController;

        //Test
        public static YummyItem? getLastObject1()
        {
            return _lastObject1;
        }

        public YummyAPIController(IGeneratorService generator)
        {
            _generator = generator;            
        }




        [HttpGet]
        public IActionResult Get()
        {
            //Debug.WriteLine("GET IS NOW!");
            //var random = new Random();
            //var items = _generator.GenerateYummyItemsList();
            //called = true;
            //_lastObject1 = items[random.Next(items.Count)];
            //return Ok(_lastObject1);
            return Ok("Nothing here");
        }

        [HttpGet("getBySize")]
        public IActionResult Get([FromQuery] int q)
        {
            //if (q <= 0) return BadRequest("Quantity should be grater than 0");

            //var random = new Random();
            //var items = _generator.GenerateYummyItemsList(q);
            //return Ok(items[random.Next(items.Count)]);
            return Ok("Nothing here");
        }
    }

}
