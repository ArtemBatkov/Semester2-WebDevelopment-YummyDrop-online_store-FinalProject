using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using YummySharedLibrary;

namespace DbContextSharLab
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<FruitBox> FruitBoxTable { get; set; }
        public DbSet<YummyItem> YummyItemTable { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}