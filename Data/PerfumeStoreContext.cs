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

        public DbSet<PerfumeStore.Models.Product> Product { get; set; } = default!;
    }
}
