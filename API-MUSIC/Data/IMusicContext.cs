using API_MUSIC.Models;
using Microsoft.EntityFrameworkCore;

namespace API_MUSIC.Data;

public class IMusicContext :DbContext  ///<== Somente realizar add-migration na classe do UsersbdContext
{
    //public IMusicContext(DbContextOptions<IMusicContext> options) : base(options) { }

    //public DbSet<Music>  MUSIC { get; set; }

    //public DbSet<Artist> ARTISTS { get; set; }

    //public DbSet<Address> ADDRESSE_ARTISTS { get; set; }

}
