using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsPro.Models;

namespace SportsPro.Models.DataLayer.Config
{
    public class RegistrationConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.HasMany(p => p.Customers)
                  .WithMany(c => c.Products)
                  .UsingEntity<Dictionary<string, object>>(
                      "Registrations",
                      r => r.HasOne<Customer>()
                            .WithMany()
                            .HasForeignKey("CustomerID"),
                      r => r.HasOne<Product>()
                            .WithMany()
                            .HasForeignKey("ProductID")
                  );
        }
    }
}
