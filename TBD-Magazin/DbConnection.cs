using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TBD_Magazin
{
    public class DbConnection: DbContext
    {
        public DbConnection():base()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 21));
            optionsBuilder.UseMySql("server=localhost;UserId=root;Password=0;database=tbd-shop;", serverVersion);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbSets.OrderProduct>().HasNoKey();
            //modelBuilder.Entity<DbSets.SupplyProduct>().HasNoKey();
            modelBuilder.Entity<DbSets.SupplyProduct>().HasKey(s => new { s.ProductId, s.SupplyId, s.Price});
        }

        public DbSet<DbSets.Category> Categories { get; set; }
        public DbSet<DbSets.Check> Checks { get; set; }
        public DbSet<DbSets.Client> Clients { get; set; }
        public DbSet<DbSets.Delivery> Deliveries { get; set; }
        public DbSet<DbSets.Manufacturer> Manufacturers { get; set; }
        public DbSet<DbSets.Order> Orders { get; set; }
        public DbSet<DbSets.OrderProduct> OrderProducts { get; set; }
        public DbSet<DbSets.Product> Products { get; set; }
        public DbSet<DbSets.Providor> Providors { get; set; }
        public DbSet<DbSets.Role> Roles { get; set; }
        public DbSet<DbSets.Supply> Supplies { get; set; }
        public DbSet<DbSets.SupplyProduct> SupplyProducts { get; set; }
        public DbSet<DbSets.Worker> Workers { get; set; }
    }
}
