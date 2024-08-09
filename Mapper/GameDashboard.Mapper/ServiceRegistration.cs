using GameDashboard.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;


namespace GameDashboard.Mapper
{
    public static class ServiceRegistration
    {
        public static void AddMapperService(this IServiceCollection services)
        {
            services.AddSingleton<IMapper, Mapper>();
        }
    }
}
