﻿using Microsoft.AspNetCore.Mvc.Infrastructure;
using Project.Application;
using Project.Infrastructure;
using Project.WebApi.Errors;

namespace Project.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        //builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>()); // replaced with error endpoint - custom problem detais factory
     
        services.AddSingleton<ProblemDetailsFactory, ApplicationProblemDetailsFactory>();
        return services;
    }
}