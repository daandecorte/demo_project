using AP.MyGameStore.Application.Interfaces;
using AP.MyGameStore.Infrastructure.Contexts;
using AP.MyGameStore.Infrastructure.Repositories;
using AP.MyGameStore.Infrastructure.UoW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AP.MyGameStore.Infrastructure.Extensions
{
    public static class Registrator
    {
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
        {
            services.RegisterDbContext();
            services.RegisterRepositories();
            return services;
        }
        public static IServiceCollection RegisterDbContext(this IServiceCollection services)
        {
            services.AddDbContext<MyGameStoreContext>(options =>
                        options.UseSqlServer("name=ConnectionStrings:MyGameStore"));

            return services;
        }

        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPeopleRepository, PeopleRepository>();
            services.AddScoped<IStoresRepository, StoresRepository>();
            services.AddScoped<IUnitofWork, UnitofWork>();
            return services;
        }
    }
}
