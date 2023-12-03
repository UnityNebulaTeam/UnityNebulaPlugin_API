using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NebulaPlugin.Api.EFCore;
using NebulaPlugin.Api.Models;
using NebulaPlugin.Api.Services.Mongo;
using NebulaPlugin.Common.Exceptions.MongoExceptions;
using src.NebulaPlugin.Common;

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
                    ErrorResponse errorResponse = new();

                    switch (contextFeature.Error)
                    {
                        case MongoNotFoundException notFoundException:
                            context.Response.StatusCode = (int)notFoundException.StatusCode;
                            errorResponse.Message = notFoundException.Message;
                            break;

                        case MongoAlreadyExistException alreadyExistException:
                            context.Response.StatusCode = (int)alreadyExistException.StatusCode;
                            errorResponse.Message = alreadyExistException.Message;
                            break;

                        case MongoOperationFailedException operationFailedException:
                            context.Response.StatusCode = (int)operationFailedException.StatusCode;
                            errorResponse.Message = operationFailedException.Message;
                            break;

                        default:
                            errorResponse.Message = contextFeature.Error.Message;
                            break;
                    }


                    await context.Response.WriteAsync(errorResponse.ToString());
                }
            });
        });
    }


    public static void ConfigureSqlConnection(this IServiceCollection services, IConfiguration configuration)
    {
        var sqlServerSettings = configuration.GetSection("SqlServerSettings");
        services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(sqlServerSettings["ConnectionString"]));

        // services.AddIdentity<User, IdentityRole>()
        //  .AddEntityFrameworkStores<AppDbContext>()
        //  .AddDefaultTokenProviders();
    }
}
