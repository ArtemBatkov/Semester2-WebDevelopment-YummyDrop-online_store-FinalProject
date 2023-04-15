using MatBlazor;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using YummySharedLibrary;

namespace DbContextSharLab
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<FruitBox> FruitBoxTable { get; set; }
        public DbSet<YummyItem> YummyItemTable { get; set; }

        public DbSet<Cart> CartTable { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<ApplicationDbContext>().OwnsOne(x => x.CartTable);
        //}


    }
}