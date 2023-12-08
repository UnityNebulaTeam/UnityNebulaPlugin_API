using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NebulaPlugin.Api.EFCore;
using NebulaPlugin.Api.Models;
using NebulaPlugin.Common.Exceptions.MongoExceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using src.NebulaPlugin.Common;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using NebulaPlugin.Api.Services.Auth;
using NebulaPlugin.Api.Services.User;
using NebulaPlugin.Api.Services;
using NebulaPlugin.Application.Mongo;

namespace NebulaPlugin.Api;

public static class Extensions
{
    public static void AddServices(this IServiceCollection services)
    {
        // services.AddScoped<IMongoService, MongoService>();
        services.AddScoped<IServiceManager, ServiceManager>();
        services.AddScoped<UserManager<User>, UserManager<User>>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();

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
                            errorResponse.Success = false;
                            break;

                        case MongoAlreadyExistException alreadyExistException:
                            context.Response.StatusCode = (int)alreadyExistException.StatusCode;
                            errorResponse.Message = alreadyExistException.Message;
                            errorResponse.Success = false;
                            break;

                        case MongoOperationFailedException operationFailedException:
                            context.Response.StatusCode = (int)operationFailedException.StatusCode;
                            errorResponse.Message = operationFailedException.Message;
                            errorResponse.Success = false;
                            break;

                        default:
                            errorResponse.Message = contextFeature.Error.Message;
                            errorResponse.Success = false;
                            break;
                    }


                    await context.Response.WriteAsync(errorResponse.ToString());
                }
            });
        });
    }

    public static void ConfigureSqlConnection(this IServiceCollection services, IConfiguration configuration)
    {
        // var sqlServerSettings = configuration.GetSection("SqlServerSettings");
        // services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(sqlServerSettings["ConnectionString"]));
        services.AddDbContext<AppDbContext>(opt => opt.UseSqlite("Data Source=Test.db"));


    }

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole>()
         .AddEntityFrameworkStores<AppDbContext>()
         .AddDefaultTokenProviders();
    }

    public static void ConfigureJWT(this IServiceCollection services, IConfiguration config)
    {
        var jwtSettings = config.GetSection("JwtSettings");
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["issuer"],
                ValidAudience = jwtSettings["audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["secret"]))
            };
        });

        services.AddAuthorization();
    }

}
