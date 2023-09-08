using API_MUSIC.Controllers.Models;
using Microsoft.EntityFrameworkCore;

namespace API_MUSIC.Data;

public class IMusicContext:DbContext
{
    public IMusicContext(DbContextOptions<IMusicContext> options):base(options) { }
    
    public DbSet<Music>  Music { get; set; }

    public DbSet<Artist> Artists { get; set; }
}
