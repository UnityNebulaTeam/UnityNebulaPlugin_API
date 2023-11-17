using NebulaPlugin.Api.Services.Mongo;

namespace NebulaPlugin.Api;

public static class Extensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IMongoService, MongoService>();
    }
}
