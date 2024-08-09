using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using GameDashboard.Application.Exceptions;
using GameDashboard.Application;
using GameDashboard.Application.Behaviors;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;

namespace GameDashboard.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplication(this IServiceCollection services)
        {

            services.AddExceptionHandler<ExceptionMiddelware>();

            services.AddMediatR(opt => opt.RegisterServicesFromAssemblies(typeof(ServiceRegistration).Assembly));


            services.AddValidatorsFromAssembly(typeof(ServiceRegistration).Assembly);


            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehavior<,>));

        }
    }
}
