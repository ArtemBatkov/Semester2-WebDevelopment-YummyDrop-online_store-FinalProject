using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;

using YummyDrop_online_store.Services.GeneratorService;
using System.Diagnostics;
using YummyDrop_online_store.Services.RandomizeService;
using YummyDrop_online_store.Data;
using Microsoft.EntityFrameworkCore;
using YummySharedLibrary;
using DbContextSharLab;

namespace YummyDrop_online_store.Controllers
{
    [ApiController]
    [Route("api/yummy")]
    public class YummyAPIController : ControllerBase
    {
       
        private readonly IGeneratorService _generator;
        private readonly IRandomizeService _randomize;
        private readonly ApplicationDbContext _dbcontext;

        private static YummyItem? _lastObject1 = 
            _lastObject1 = new YummyItem { Name = "Default" };

        private static bool called = false;

    private YummyAPIController? yummyAPIController;

        //Test
        public static YummyItem? getLastObject1()
        {
            return _lastObject1;
        }

        private List<YummyItem> LootList = new List<YummyItem>();


        public YummyAPIController(IGeneratorService generator, IRandomizeService randomize, ApplicationDbContext dbcontext)
        {
            _generator = generator;    
            _randomize = randomize;
            _dbcontext = dbcontext;

            Task.Run(async () =>
            {
                await LoadFromDb();
            }).Wait();
        }

        private async Task LoadFromDb()
        {
            var boxes = await _dbcontext
                .FruitBoxTable.Include(box => box.BoxContent1).ToListAsync();
            var box1 = boxes[0];
            var boxcont = box1.BoxContent1;
            LootList = boxcont;
        }


        private Random random = new Random();

        [HttpGet]
        public IActionResult Get()
        {
            if (LootList == null) return Ok("Nothing here. Refresh the page!");
            var shuffledIds = _generator.GenerateMillionIds(LootList);
            int Id = _randomize.GetRandomId(shuffledIds);
            YummyItem item = LootList.FirstOrDefault(x => x.Id == Id);
            if(item == null)
            {
                return Ok("Nothing here");
            }
            else
            {
                return Ok(item);
            }
        }

        


        
    }

}
