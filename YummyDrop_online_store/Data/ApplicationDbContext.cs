using Microsoft.EntityFrameworkCore;
using YummyDrop_online_store.Models;

namespace YummyDrop_online_store.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet <FruitBox> FruitBoxTable { get; set; }
        public DbSet <YummyItem> YummyItemTable { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }
    }
}
