using System;
using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using EcommerceDomainDrivenDesign.Domain.Customers;
using EcommerceDomainDrivenDesign.Domain;
using EcommerceDomainDrivenDesign.Domain.Core.Messaging;
using EcommerceDomainDrivenDesign.Domain.Products;
using EcommerceDomainDrivenDesign.Infrastructure.Domain;
using EcommerceDomainDrivenDesign.Infrastructure.Domain.Customers;
using EcommerceDomainDrivenDesign.Infrastructure.Domain.Products;
using EcommerceDomainDrivenDesign.Infrastructure.Messaging;
using Microsoft.AspNetCore.Authorization;
using EcommerceDomainDrivenDesign.Infrastructure.Identity.Claims;
using EcommerceDomainDrivenDesign.Infrastructure.Identity.IdentityUser;
using EcommerceDomainDrivenDesign.Infrastructure.Identity.Services;
using EcommerceDomainDrivenDesign.Infrastructure.Domain.ForeignExchanges;
using EcommerceDomainDrivenDesign.Application.Customers.RegisterCustomer;
using EcommerceDomainDrivenDesign.Application.Customers.DomainServices;
using EcommerceDomainDrivenDesign.Domain.CurrencyExchange;

namespace EcommerceDomainDrivenDesign.Infrastructure.IoC
{
    public static class ServicesInjectionExtension
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            //Domain services
            services.AddScoped<ICustomerUniquenessChecker, CustomerUniquenessChecker>();
            services.AddScoped<ICurrencyConverter, CurrencyConverter>();

            // Application - Handlers            
            services.AddMediatR(typeof(RegisterCustomerCommandHandler).GetTypeInfo().Assembly);

            // Infra - Domain persistence
            services.AddScoped<IEcommerceUnitOfWork, EcommerceUnitOfWork>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            // Infrastructure - Data EventSourcing
            services.AddScoped<IStoredEventRepository, StoredEventRepository>();
            services.AddSingleton<IEventSerializer, EventSerializer>();

            // Infrastructure - Identity     
            services.AddSingleton<IAuthorizationHandler, ClaimsRequirementHandler>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUser, User>();

            //Messaging
            services.AddScoped<IMessagePublisher, MessagePublisher>();
            services.AddScoped<IMessageProcessor, MessageProcessor>();
        }
    }
}
