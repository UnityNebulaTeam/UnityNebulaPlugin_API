using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NebulaPlugin.Api.Dtos.User;
using NebulaPlugin.Api.Dtos.User.Response;
using NebulaPlugin.Api.EFCore;
using NebulaPlugin.Api.Helpers;
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

    public async Task AddConnectionAsync(AddUserDatabaseDto database, string userId)
    {
        bool userDbExist = await _context.Connections.AnyAsync(db => db.KeyIdentifier == database.KeyIdentifier);

        if (userDbExist)
            throw new Exception($"{database.KeyIdentifier} database already exist for this user");

        string encryptedConnection = AesCrypter.EncryptToString(database.ConnectionString);

        Console.WriteLine("--------------------->" + encryptedConnection);

        Connection db = new()
        {
            KeyIdentifier = database.KeyIdentifier,
            ConnectionString = encryptedConnection,
            UserId = userId
        };

        _context.Connections.Add(db);
        await _context.SaveChangesAsync();

    }

    public async Task UpdateConnectionAsync(UpdateUserDatabaseDto database, string userId, string type)
    {
        Connection? db = await _context.Connections.Where(db => db.UserId == userId).FirstOrDefaultAsync(db => db.KeyIdentifier == type.ToUpper());

        if (db is null)
            throw new Exception($"{type} database record not found for this user. ");

        db.ConnectionString = AesCrypter.EncryptToString(database.ConnectionString);

        _context.Connections.Update(db);



        var res = await _context.SaveChangesAsync();

        if (res < 0)
            throw new Exception($"Failed to update db '{db.KeyIdentifier}'.");

    }

    public async Task<List<UserDatabaseResponse>> GetUsersConnections(string userId)
    {
        var userDbs = await _context.Connections.Where(db => db.UserId == userId).ToListAsync();
        List<UserDatabaseResponse> res = new();

        userDbs.ForEach(db => res.Add(new UserDatabaseResponse(db.KeyIdentifier, AesCrypter.DecryptBytesToString(db.ConnectionString))));

        return res;

    }

    public async Task<UserResult> GetUserDataAsync(string userId)
    {
        Models.User? dbUser = await _userManager.Users.Include(u => u.Connections).FirstOrDefaultAsync(u => u.Id == userId);

        var userDatabases = dbUser.Connections.ToList();

        List<UserDatabaseResponse> userDbs = new();

        userDatabases.ForEach(ud => userDbs.Add(new UserDatabaseResponse(ud.KeyIdentifier, AesCrypter.DecryptBytesToString(ud.ConnectionString))));

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
