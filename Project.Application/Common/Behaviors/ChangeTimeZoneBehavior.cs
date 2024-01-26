using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Common.Behaviors;

public sealed class ChangeTimeZoneBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly TRequest _request;

    public ChangeTimeZoneBehavior(TRequest request)
    {
        _request = request;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // Call the next handler in the pipeline
        var response = await next();

        // Get the type of the request and the response
        var requestType = typeof(TRequest);
        var responseType = typeof(TResponse);

        // Get the properties of the request and the response
        var requestProperties = requestType.GetProperties();
        var responseProperties = responseType.GetProperties();

        // Define the time span to add to the date time values
        var timeSpan = TimeSpan.FromHours(2);

        // Iterate over the request properties
        foreach (var property in requestProperties)
        {
            // Check if the property type is DateTime
            if (property.PropertyType == typeof(DateTime))
            {
                // Get the current value of the property
                var currentValue = (DateTime)property.GetValue(request)!;

                // Add the time span to the current value
                var newValue = currentValue.Add(timeSpan);

                // Set the new value to the property
                property.SetValue(request, newValue);
            }
        }

        // Iterate over the response properties
        foreach (var property in responseProperties)
        {
            // Check if the property type is DateTime
            if (property.PropertyType == typeof(DateTime))
            {
                // Get the current value of the property
                var currentValue = (DateTime)property.GetValue(response)!;

                // Add the time span to the current value
                var newValue = currentValue.Add(timeSpan);

                // Set the new value to the property
                property.SetValue(response, newValue);
            }
        }

        // Return the modified response
        return response;
    }
}
