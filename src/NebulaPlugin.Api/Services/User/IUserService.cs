using NebulaPlugin.Api.Dtos.Mongo.Responses;
using NebulaPlugin.Api.Dtos.User;
using NebulaPlugin.Api.Dtos.User.Response;

namespace NebulaPlugin.Api.Services.User;

public interface IUserService
{
    Task AddConnectionAsync(AddUserDatabaseDto database, string userId);
    // Task<string> GetConnectionStringByDatabaseIdAsync(Guid id);
    // string GetConnectionStringByDatabaseId(Guid id);
    Task<List<UserDatabaseResponse>> GetUsersConnections(string userId);
    Task<UserResult> GetUserDataAsync(string userId);

    Task UpdateConnectionAsync(UpdateUserDatabaseDto database, string userId, string type);
}
