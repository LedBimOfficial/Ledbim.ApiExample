using AutoMapper;
using FluentValidation;
using Ledbim.ApiExample.Application.Mappers;
using Ledbim.ApiExample.Application.PipelineBehaviors;
using Ledbim.ApiExample.Infrastructure;
using Ledbim.Core.Security;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Ledbim.ApiExample.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddValidatorsFromAssembly(assembly);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            // AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // TokenSettings
            services.Configure<TokenSettings>(configuration.GetSection("JWT"));
            services.AddSingleton<ITokenSettings>(sp =>
                sp.GetRequiredService<IOptions<TokenSettings>>().Value);

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddInfrastructure(configuration);

            return services;
        }
    }
}
