using Microsoft.Extensions.Configuration;
using EcommerceDomainDrivenDesign.Domain;
using EcommerceDomainDrivenDesign.Domain.Core.Messaging;
using EcommerceDomainDrivenDesign.Domain.Customers;
using EcommerceDomainDrivenDesign.Domain.Products;
using Microsoft.Extensions.DependencyInjection;
using EcommerceDomainDrivenDesign.Infrastructure.Database.Context;
using EcommerceDomainDrivenDesign.Infrastructure.Domain.Customers;
using EcommerceDomainDrivenDesign.Infrastructure.Messaging;
using EcommerceDomainDrivenDesign.Infrastructure.Domain.Products;
using EcommerceDomainDrivenDesign.Infrastructure.Domain;
using Microsoft.EntityFrameworkCore;
using EcommerceDomainDrivenDesign.Domain.CurrencyExchange;
using EcommerceDomainDrivenDesign.Infrastructure.Domain.ForeignExchanges;

namespace EcommerceDomainDrivenDesign.DataSeed
{
    class Program
    {
        private static IServiceProvider _serviceProvider;

        static async Task Main(string[] args)
        {
            RegisterServices();
            var unitOfWork = _serviceProvider.GetService<IEcommerceUnitOfWork>();
            var currencyConverter = _serviceProvider.GetService<ICurrencyConverter>();
            await SeedProducts.SeedData(unitOfWork, currencyConverter);
            DisposeServices();
        }

        private static void RegisterServices()
        {
            var services = new ServiceCollection();
            var connString = GetDbConnection();
            services.AddDbContext<EcommerceDDDContext>(options =>
            options.UseSqlServer(connString));

            services.AddScoped<IEcommerceUnitOfWork, EcommerceUnitOfWork>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IStoredEventRepository, StoredEventRepository>();
            services.AddScoped<IEventSerializer, EventSerializer>();
            services.AddScoped<ICurrencyConverter, CurrencyConverter>();
            services.AddScoped<EcommerceDDDContext>();

            _serviceProvider = services.BuildServiceProvider();
        }

        private static void DisposeServices()
        {
            if (_serviceProvider == null)
                return;
            if (_serviceProvider is IDisposable)
                ((IDisposable)_serviceProvider).Dispose();
        }

        private static string GetDbConnection()
        {
            var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            string strConnection = builder.Build().GetConnectionString("DefaultConnection");

            return strConnection;
        }
    }
}
