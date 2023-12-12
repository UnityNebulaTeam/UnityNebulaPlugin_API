using NebulaPlugin.Api.Dtos.Mongo.Responses;
using NebulaPlugin.Api.Dtos.User;
using NebulaPlugin.Api.Dtos.User.Response;

namespace NebulaPlugin.Api.Services.User;

public interface IUserService
{
    Task AddDatabaseAsync(AddUserDatabaseDto database, string userId);
    // Task<string> GetConnectionStringByDatabaseIdAsync(Guid id);
    // string GetConnectionStringByDatabaseId(Guid id);
    Task<List<UserDatabaseResponse>> GetUsersDatabases(string userId);
    Task<UserResult> GetUserDataAsync(string userId);

    Task UpdateDatabaseAsync(UpdateUserDatabaseDto database, string userId, string type);
}
