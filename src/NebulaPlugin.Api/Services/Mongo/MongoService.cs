using NebulaPlugin.Application.Mongo;

namespace NebulaPlugin.Api.Services.Mongo;
public class MongoService : IMongoService
{
    public void TestGetDatabases()
    {
        MongoManagement mongo = new MongoManagement();
        mongo.Create();
    }
}
