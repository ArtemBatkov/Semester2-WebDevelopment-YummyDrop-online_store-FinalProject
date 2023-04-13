using YummyDrop_online_store.Models;
using System.Linq;

namespace YummyDrop_online_store.Data
{
    public class FruitBoxRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public FruitBoxRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<FruitBox> GetAllFruitBoxes()
        {
            return _dbContext.FruitBoxTable;
        }
    }
}
