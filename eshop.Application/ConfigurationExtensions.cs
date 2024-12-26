using eshop.Application.Categories;
using eshop.Application.eshop.Application.Products;
using eshop.Domain.Entities;
using eshop.Infrastructure;
using eshop.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Proxies;
using eshop.Domain.Repositories;
using eshop.Application.Users;


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
            services.AddScoped<IProductRepository<Product>, ProductRepository>()
                .AddScoped<ICategoryRepository<Category>, CategoryRepository>()
                .AddScoped<IUserRepository<User>, UserRepository>();
            //Services
            services.AddScoped<GetProductService>().AddScoped<AddProductService>().
                AddScoped<AddCategoryService>().AddScoped<GetCategoryService>()
                .AddScoped<UserRegistrationService>().AddScoped<GetUsersService>()
                .AddTransient<UserAuthenticationService>();
            return services;
        }
    }


   
    
}
