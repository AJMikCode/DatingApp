using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using API.Interfaces;
using API.Data;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config) 
        {
            //Scoped ot the lifetime of the HTTP Request in this case, new instance of service crated, when request finished, service disposed.
            services.AddScoped<ITokenService, TokenService>();
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}