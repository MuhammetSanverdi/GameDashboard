
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using GameDashboard.Persistence.Context;
using GameDashboard.Persistence;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using GameDashboard.Application.Repositories;
using GameDashboard.Persistence.Repositories.MongoDb;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Core.Configuration;
using GameDashboard.Domain.Concrete.Identity;
using GameDashboard.Application.UnitOfWorks;
using GameDashboard.Persistence.Repositories.MySQL;
using GameDashboard.Persistence.UnitOfWorks;
using IkProject.Persistence.Repositories;


namespace GameDashboard.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistence(this IServiceCollection services,IConfiguration configuration)
        {
           

            string connectionString = configuration.GetConnectionString("GameDashboardDb");
            services.AddDbContext<GameDashboardDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));



            services.AddIdentity<AppUser, AppRole>()
                     .AddEntityFrameworkStores<GameDashboardDbContext>()
                    .AddDefaultTokenProviders();


            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));

            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped(typeof(INoSQLRepository<>),typeof(MongoRepository<>));                       


            services.AddSingleton<IMongoDatabase>(s => new ConfigrationMongoDbService(configuration).Database);            
        }
    }
}

