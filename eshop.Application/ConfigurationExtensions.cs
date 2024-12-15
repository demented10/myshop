using eshop.Application.Categories;
using eshop.Application.eshop.Application.Products;
using eshop.Domain.Entities;
using eshop.Infrastructure;
using eshop.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Proxies;


namespace eshop.Application
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString)
            .UseLazyLoadingProxies()
            );

            //Repositories
            services.AddScoped<IRepository<Product>, ProductRepository>()
                .AddScoped<ICategoryRepository<Category>, CategoryRepository>();
            //Services
            services.AddScoped<GetProductService>().AddScoped<AddProductService>().
                AddScoped<AddCategoryService>().AddScoped<GetCategoryService>();
                     
            return services;
        }
    }


   
    
}
