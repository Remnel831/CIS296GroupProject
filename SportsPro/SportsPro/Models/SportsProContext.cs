using Microsoft.EntityFrameworkCore;
using SportsPro.Models.DataLayer.Config;
using SportsPro.Models.DataLayer.SeedData;

namespace SportsPro.Models
{
    public class SportsProContext : DbContext
    {
        public SportsProContext(DbContextOptions<SportsProContext> options)
            : base(options)
        { }

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Technician> Technicians { get; set; } = null!;
        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Incident> Incidents { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RegistrationConfig());
            modelBuilder.ApplyConfiguration(new SeedProducts());
            modelBuilder.ApplyConfiguration(new SeedTechnicians());
            modelBuilder.ApplyConfiguration(new SeedCountries());
            modelBuilder.ApplyConfiguration(new SeedCustomers());
            modelBuilder.ApplyConfiguration(new SeedIncidents());
        }
    }
}