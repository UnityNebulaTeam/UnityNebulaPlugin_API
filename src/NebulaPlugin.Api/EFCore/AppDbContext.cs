using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NebulaPlugin.Api.Models;

namespace NebulaPlugin.Api.EFCore;

public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // protected override void OnModelCreating(ModelBuilder builder)
    // {
    //     base.OnModelCreating(builder);

    //     builder.Entity<User>().Property(u => u.Id).ValueGeneratedNever().HasConversion<Guid>();

    //     builder.Entity<Database>()
    // .HasOne(d => d.User)
    // .WithMany() 
    // .HasForeignKey(d => d.UserId)
    // .IsRequired();

    // }

    public DbSet<Database> Databases { get; set; }
}
