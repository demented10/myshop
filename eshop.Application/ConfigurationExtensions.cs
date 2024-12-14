using eshop.Application.eshop.Application.Products;
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
            Console.WriteLine("RegisterApplicationServices");
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql());
            services.AddScoped<RepositoryFactory, DatabaseRepositoryFactory>();
            services.AddScoped<GetProductService>();
            services.AddScoped<AddProductService>();
                     
            return services;
        }
    }


   
    
}
