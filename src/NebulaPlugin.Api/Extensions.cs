using System.Net;
using System.Text.Json;
using Amazon.Runtime.Internal;
using Microsoft.AspNetCore.Diagnostics;
using NebulaPlugin.Api.Services.Mongo;

namespace NebulaPlugin.Api;

public static class Extensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IMongoService, MongoService>();
    }

    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                if (contextFeature != null)
                {
                    ErrorResponse errorResponse = new()
                    {
                        StatusCode = (HttpStatusCode)context.Response.StatusCode,
                        Message = contextFeature.Error.Message
                    };

                    var json = JsonSerializer.Serialize(errorResponse);
                    Console.WriteLine("-------->" + json);
                    await context.Response.WriteAsync(json);
                }
            });
        });
    }
}
