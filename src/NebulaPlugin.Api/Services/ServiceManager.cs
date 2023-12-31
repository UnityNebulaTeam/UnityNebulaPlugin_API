using NebulaPlugin.Api.EFCore;
using NebulaPlugin.Api.Services.Mongo;
using NebulaPlugin.Application.Mongo;

namespace NebulaPlugin.Api.Services;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IMongoService> _mongoService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public ServiceManager(IHttpContextAccessor httpContextAccessor, AppDbContext context)
    {
        _httpContextAccessor = httpContextAccessor;

        _mongoService = new Lazy<IMongoService>(() => new MongoService(_httpContextAccessor, context));
    }
    public IMongoService Mongo => _mongoService.Value;


}
