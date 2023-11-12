using API_MUSIC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace API_MUSIC.Data;
//(https://learn.microsoft.com/pt-br/aspnet/core/security/authentication/customize-identity-model?view=aspnetcore-6.0)
//public class UsersDbContext : IdentityDbContext<Users,IdentityRole<int>,int>
//{
//    public UsersDbContext(DbContextOptions<UsersDbContext> opt) : base(opt)
//    {

//    }
//    //protected override void OnModelCreating(ModelBuilder builder)
//    //{
//    //     builder.Entity<Users>()
//    //    .HasOne(a => a.Administrator) //<= Relacionamento 1:
//    //    .WithOne(b => b.User) //<= Relacionamento :1
//    //    .HasForeignKey<Administrator>(b => b.IdUser);
//    //}
//}
public class UsersDbContext : IdentityDbContext<User>  ///<== Somente realizar add-migration nesta classe
{
    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
    {
      
    }
    public DbSet<Music> MUSIC { get; set; }

    public DbSet<Artist> ARTISTS { get; set; }

    public DbSet<Address> ADDRESSE_ARTISTS { get; set; }
    public DbSet<Administrator> ADMINISTRATORS { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Administrator>()
             .HasOne(u => u.User)
             .WithOne(a => a.Administrator)
             .HasForeignKey<Administrator>(a => a.IdUser);


        modelBuilder.Entity<Artist>()
            .HasOne(u => u.User)
            .WithOne(a => a.Artist)
            .HasForeignKey<Artist>(a => a.UserID);
    }
}
