using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using NebulaPlugin.Api.Dtos.User;
using NebulaPlugin.Api.Dtos.User.Response;
using NebulaPlugin.Api.EFCore;
using NebulaPlugin.Api.Models;

namespace NebulaPlugin.Api.Services.User;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UserDatabaseResponse> AddDatabaseAsync(AddUserDatabaseDto database, string userId)
    {
        Database db = new()
        {
            KeyIdentifier = database.KeyIdentifier,
            ConnectionString = database.ConnectionString,
            UserId = userId
        };
        _context.Databases.Add(db);
        await _context.SaveChangesAsync();

        return new(db.KeyIdentifier, db.ConnectionString);
    }

    public async Task<List<UserDatabaseResponse>> GetUsersDatabases(string userId)
    {
        var userDbs = await _context.Databases.Where(db => db.UserId == userId).ToListAsync();
        List<UserDatabaseResponse> res = new();
        userDbs.ForEach(db => res.Add(new UserDatabaseResponse(db.KeyIdentifier, db.ConnectionString)));

        return res;

    }
}
