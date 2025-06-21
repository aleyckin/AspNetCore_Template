using Interfaces.IService;
using Microsoft.Extensions.DependencyInjection;
using Service.MapperProfiles;
using Service.Services;
using Service.Services.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Service
{
    public static class ServiceDependency
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IJwtGenerator, JwtGenerator>();

            services.AddAutoMapper(typeof(UserMapperProfile));

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
            });
            
            return services;
        }
    }
}
