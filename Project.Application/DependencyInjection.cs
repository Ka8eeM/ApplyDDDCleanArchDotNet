using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Project.Application.Common.Behaviors;
using Project.Application.Mapping;
using System.Reflection;

namespace Project.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(assembly);

            config.AddOpenBehavior(typeof(QueryCachingPipelineBehavior<,>));

            //config.AddOpenBehavior(typeof(UnitOfWorkBehavior<,>));
            //config.AddOpenBehavior(typeof(ChangeTimeZoneBehavior<,>));
        });



        services.AddScoped(
                typeof(IPipelineBehavior<,>),
                typeof(ValidationPipelineBehavior<,>));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(),
            includeInternalTypes: true); // instead register every single validator alone register all with reflection

        //services.AddScoped<IValidator<RegisterCommand>, RegisterCommandValidator>();



        services.AddMappings();

        return services;
    }
}
