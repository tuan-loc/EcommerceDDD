using System;
using AutoMapper;
using EcommerceDomainDrivenDesign.Application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace EcommerceDomainDrivenDesign.WebApp.Configurations
{
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(typeof(RequestToCommandProfile));
        }
    }
}
