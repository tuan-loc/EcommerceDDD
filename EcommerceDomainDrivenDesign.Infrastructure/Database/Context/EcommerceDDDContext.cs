using EcommerceDomainDrivenDesign.Domain.Core.Messaging;
using EcommerceDomainDrivenDesign.Domain.Customers;
using EcommerceDomainDrivenDesign.Domain.Customers.Orders;
using EcommerceDomainDrivenDesign.Domain.Payments;
using EcommerceDomainDrivenDesign.Domain.Products;
using EcommerceDomainDrivenDesign.Infrastructure.Database.Configurations;
using Microsoft.EntityFrameworkCore;

namespace EcommerceDomainDrivenDesign.Infrastructure.Database.Context
{
    public sealed class EcommerceDDDContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<StoredEvent> StoredEvents { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public EcommerceDDDContext(DbContextOptions<EcommerceDDDContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Event>();
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new StoredMessageConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentConfiguration());
        }
    }
}
