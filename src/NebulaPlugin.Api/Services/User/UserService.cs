using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NebulaPlugin.Api.Dtos.User;
using NebulaPlugin.Api.Dtos.User.Response;
using NebulaPlugin.Api.EFCore;
using NebulaPlugin.Api.Models;

namespace NebulaPlugin.Api.Services.User;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly UserManager<Models.User> _userManager;

    public UserService(AppDbContext context, UserManager<Models.User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task AddDatabaseAsync(AddUserDatabaseDto database, string userId)
    {
        bool userDbExist = await _context.Databases.AnyAsync(db => db.KeyIdentifier == database.KeyIdentifier);

        if (userDbExist)
            throw new Exception($"{database.KeyIdentifier} database already exist for this user");

        Database db = new()
        {
            KeyIdentifier = database.KeyIdentifier,
            ConnectionString = database.ConnectionString,
            UserId = userId
        };

        _context.Databases.Add(db);
        await _context.SaveChangesAsync();

    }

    public async Task UpdateDatabaseAsync(UpdateUserDatabaseDto database, string userId, string type)
    {
        Database? db = await _context.Databases.Where(db => db.UserId == userId).FirstOrDefaultAsync(db => db.KeyIdentifier == type.ToUpper()); 

        if (db is null)
            throw new Exception($"{type} database record not found for this user. ");

        db.ConnectionString = database.ConnectionString;

        _context.Databases.Update(db);



        var res = await _context.SaveChangesAsync();

        if (res < 0)
            throw new Exception($"Failed to update db '{db.KeyIdentifier}'.");

    }

    public async Task<List<UserDatabaseResponse>> GetUsersDatabases(string userId)
    {
        var userDbs = await _context.Databases.Where(db => db.UserId == userId).ToListAsync();
        List<UserDatabaseResponse> res = new();
        userDbs.ForEach(db => res.Add(new UserDatabaseResponse(db.KeyIdentifier, db.ConnectionString)));

        return res;

    }

    public async Task<UserResult> GetUserDataAsync(string userId)
    {
        Models.User? dbUser = await _userManager.Users.Include(u => u.Databases).FirstOrDefaultAsync(u => u.Id == userId);

        var userDatabases = dbUser.Databases.ToList();

        List<UserDatabaseResponse> userDbs = new();

        userDatabases.ForEach(ud => userDbs.Add(new UserDatabaseResponse(ud.KeyIdentifier, ud.ConnectionString)));

        if (dbUser is null)
            throw new Exception("User not found");


        UserResult userResult = new()
        {
            UserName = dbUser.UserName,
            Email = dbUser.Email,
            EmailConfirmed = dbUser.EmailConfirmed,
            Dbs = userDbs
        };

        return userResult;
    }

}
