using WEBAPI_FinalWork.BLL.Services;
using WEBAPI_FinalWork.DAL.Repositories;

namespace WEBAPI_FinalWork.API.Extensions
{
    public static class ServiceBuilderExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<CarService>();
            services.AddScoped<ManufactureService>();
            services.AddScoped<ImageService>();
            return services;
        }
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<CarRepository>();
            services.AddScoped<ManufactureRepository>();
            return services;
        }
    }
}
