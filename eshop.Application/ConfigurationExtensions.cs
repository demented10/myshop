using eshop.Application.eshop.Application.Products;
using eshop.Domain.Entities;
using eshop.Infrastructure;
using eshop.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace eshop.Application
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

            //services.AddScoped<RepositoryFactory, DatabaseRepositoryFactory>();
            services.AddScoped<IRepository<Product>, ProductRepository>();
            services.AddScoped<GetProductService>();
            services.AddScoped<AddProductService>();
                     
            return services;
        }
    }


   
    
}
