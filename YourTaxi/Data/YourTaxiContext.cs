using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YourTaxi.Models;

namespace YourTaxi.Data
{
    public class YourTaxiContext : DbContext
    {
        public YourTaxiContext (DbContextOptions<YourTaxiContext> options)
            : base(options)
        {
        }
        public DbSet<Cars> Cars { get; set; }
        public DbSet<CarBook> CarBook { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<Login> Login { get; set; }
    }
}
