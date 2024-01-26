using Newtonsoft.Json.Linq;
using System.IO.Pipelines;
using System.Text;

namespace Project.WebApi.Middleware;

public class ResponseTimeZoneHandlingMiddleware
{

    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;
    private int TimeZone { get; } = 2;

    public ResponseTimeZoneHandlingMiddleware(
        RequestDelegate next,
        IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
        TimeZone = _configuration.GetValue<int>("TimeZoneConfig");
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Create a buffer for the response body
        var originalBody = context.Response.Body;
        using (var buffer = new MemoryStream())
        {
            context.Response.Body = buffer;

            // Call the next middleware in the pipeline
            await _next(context);

            // Call function Y here
            var modifiedBody = await addTimeZoneHoursToTheResponse(context);
            if (string.IsNullOrEmpty(modifiedBody))
            {
                return;
            }
            var modified = new MemoryStream(Encoding.UTF8.GetBytes(modifiedBody));
            // Replace the response body with the modified body
            
            buffer.Position = 0;
            //context.Response.Body = modified;
            await buffer.CopyToAsync(modified);
        }
    }

    private async Task<string> addTimeZoneHoursToTheResponse(HttpContext context)
    {
        try
        {
            // Get the response body as a string
            // Get the response body as a string
            context.Response.Body.Position = 0; // Add this line to reset the stream position
            var body = await new StreamReader(context.Response.Body).ReadToEndAsync();

            if (string.IsNullOrEmpty(body))
                return "";
            // Parse the body as a JSON array
            var bodyJson = JArray.Parse(body);

            // Iterate over the elements of the body JSON array
            foreach (var element in bodyJson)
            {
                // Check if the element is a JSON object
                if (element is JObject obj)
                {
                    // Iterate over the properties of the element JSON object
                    foreach (var prop in obj.Properties())
                    {
                        // Check if the property value is a DateTime
                        if (DateTime.TryParse(prop.Value.ToString(), out var date))
                        {
                            // Add 2 hours to the date and assign it back to the property value
                            prop.Value = date.AddHours(2);
                        }
                    }
                }
            }
            // Convert the modified body JSON object back to a string
            var modifiedBody = bodyJson.ToString();
            return modifiedBody;

            //// Get the response headers as a dictionary
            //var headers = context.Response.Headers.ToDictionary(h => h.Key, h => h.Value);

            //// Iterate over the headers dictionary
            //foreach (var pair in headers)
            //{
            //    // Check if the header value is a DateTime
            //    if (DateTime.TryParse(pair.Value, out var date))
            //    {
            //        // Add 2 hours to the date and assign it back to the header value
            //        headers[pair.Key] = date.AddHours(2).ToString();
            //    }
            //}

            //// Replace the response headers with the modified headers
            //context.Response.Headers.Clear();
            //context.Response.Headers.AppendDictionary(headers);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return "";
        }

    }

}
