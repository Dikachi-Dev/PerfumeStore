using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PerfumeStore.Models;

namespace PerfumeStore.Data
{
    public class PerfumeStoreContext : DbContext
    {
        public PerfumeStoreContext (DbContextOptions<PerfumeStoreContext> options)
            : base(options)
        {
        }

        public DbSet<PerfumeStore.Models.Product> Products { get; set; } = default!;

        public DbSet<PerfumeStore.Models.Order> Orders { get; set; } = default!;
        public DbSet<Stock> Stocks { get; set; }= default!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Product>()
            //    .HasOne(p => p.Stocks)
                
            //    .WithOne(c => c.Product)
            //    .HasForeignKey<Category>(c => c.Id);
            //modelBuilder.Entity<Stock>()
            //    .HasOne(p => p.ProductId)
            //    .WithOne(s => s.Stock)
            //    .HasForeignKey<Product>(p => p.Id);
        }
    }
  
}
