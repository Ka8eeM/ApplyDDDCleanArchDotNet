using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Project.Application;
using Project.Infrastructure;
using Project.WebApi;
using Project.WebApi.Middleware;



// Add services to the container.
var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
           .AddPresentation()
           .AddApplication()
           .AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
    // app.UseMiddleware<ErrorHandlingMiddleware>(); // replaced with error handling attribute

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    //app.UseAuthentication();
    //app.UseAuthorization();
    app.MapControllers();
    //app.UseMiddleware<ResponseTimeZoneHandlingMiddleware>();
    app.Run();
}