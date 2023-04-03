using Microsoft.AspNetCore.Mvc;
using YummyDrop_online_store.Services.GeneratorService;

namespace YummyDrop_online_store.Controllers
{
    [ApiController]
    [Route("api/yummy")]
    public class YummyAPIController : ControllerBase
    {
        private readonly IGeneratorService _generator;

        public YummyAPIController(IGeneratorService generator)
        {
            _generator = generator;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var random = new Random();
            var items = _generator.GenerateYummyItemsList();
            return Ok(items[random.Next(items.Count)]);
        }

        [HttpGet("size")]
        public IActionResult Get([FromQuery] int q)
        {
            if (q <= 0) return BadRequest("Quantity should be grater than 0");

            var random = new Random();
            var items = _generator.GenerateYummyItemsList(q);
            return Ok(items[random.Next(items.Count)]);
        }
    }

}
