using MongoDB.Bson;
using NebulaPlugin.Application.Mongo;

namespace NebulaPlugin.Api.Services.Mongo;
public class MongoService : IMongoService
{
    private const string connectionURL = "mongodb+srv://Victory:7Aynras2-@gameserver.ptrocqn.mongodb.net/?retryWrites=true&w=majority";
    public async Task TestGetDatabases()
    {
        MongoManagement mongo = new MongoManagement(connectionURL);
    }
}
