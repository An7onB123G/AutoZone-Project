using AutoZone.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AutoZone.Data
{
    public class ApplicationDbContext : IdentityDbContext<Client>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<BrandModel> BrandModels { get; set; }
        public DbSet<EngineType> EngineTypes { get; set; }
        public DbSet<CarType> CarTypes { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
    }
}
