using NebulaPlugin.Api.Services.Mongo;

namespace NebulaPlugin.Api.Services;

public interface IServiceManager
{
    public IMongoService Mongo { get; }
}
