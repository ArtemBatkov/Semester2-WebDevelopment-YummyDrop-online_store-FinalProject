using Microsoft.AspNetCore.Mvc;
using YummyDrop_online_store.Models;
using YummyDrop_online_store.Services.GeneratorService;

namespace YummyDrop_online_store.Controllers
{
    [ApiController]
    [Route("api/yummy")]
    public class YummyAPIController : ControllerBase
    {
        private readonly IGeneratorService _generator;
        private YummyItem? _lastObject1;
        private YummyAPIController? yummyAPIController;

        //Test
        public YummyItem? getLastObject1()
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
            var random = new Random();
            var items = _generator.GenerateYummyItemsList();
            _lastObject1 = items[random.Next(items.Count)];
            return Ok(_lastObject1);
        }

        [HttpGet("getBySize")]
        public IActionResult Get([FromQuery] int q)
        {
            if (q <= 0) return BadRequest("Quantity should be grater than 0");

            var random = new Random();
            var items = _generator.GenerateYummyItemsList(q);
            return Ok(items[random.Next(items.Count)]);
        }
    }

}
