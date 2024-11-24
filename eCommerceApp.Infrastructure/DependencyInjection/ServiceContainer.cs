﻿using eCommerceApp.Domain.Interfaces;
using eCommerceApp.Domain.Models;
using eCommerceApp.Infrastructure.Data;
using eCommerceApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerceApp.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrasructureService (this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = "DefaultConnection";
            services.AddDbContext<AppDbContext>(options =>
            
                options.UseSqlServer(configuration.GetConnectionString(connectionString),
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(ServiceContainer).Assembly.FullName);
                        sqlOptions.EnableRetryOnFailure();
                    })
            ,ServiceLifetime.Scoped);
            services.AddScoped<IGeneric<Product>, GenericRepository<Product>>();
            services.AddScoped<IGeneric<Category>, GenericRepository<Category>>();
            return services;
        }
    }
}