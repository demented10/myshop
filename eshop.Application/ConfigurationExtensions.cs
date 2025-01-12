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
using eshop.Application.Baskets;
using eshop.Application.BasketItems;
using eshop.Application.Orders;
using eshop.Application.OrderItems;


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
                .AddScoped<IUserRepository<User>, UserRepository>()
                .AddScoped<IBasketRepository<Basket>, BasketRepository>()
                .AddScoped<IBasketItemRepository<BasketItem>, BasketItemRepository>()
                .AddScoped<IOrderRepository<Order>, OrderRepository>()
                .AddScoped<IOrderItemRepository<OrderItem>, OrderItemRepository>();
            //Services (scoped)
            services.AddScoped<GetProductService>().AddScoped<AddProductService>(). //Product
                AddScoped<AddCategoryService>().AddScoped<GetCategoryService>() //Category
                .AddScoped<UserRegistrationService>().AddScoped<GetUsersService>() //User
                .AddScoped<CreateBasketService>().AddScoped<GetBasketService>().AddScoped<DeleteBasketService>() //Basket
                .AddScoped<CreateBasketItemService>().AddScoped<QuantityBasketItemService>().AddScoped<RemoveBasketItemService>()//BasketItem
                .AddScoped<CreateOrderService>().AddScoped<GetOrderService>() //Order
                .AddScoped<CreateOrderItemService>(); //OrderItem
            //Services (transient)
            services.AddTransient<UserAuthenticationService>();



            return services;
        }
    }


   
    
}
