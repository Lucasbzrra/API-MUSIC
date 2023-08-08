using API_MUSIC.Controllers.Models;
using Microsoft.EntityFrameworkCore;

namespace API_MUSIC.Data;

public class MusicContext:DbContext
{
    public MusicContext(DbContextOptions<MusicContext> options):base(options) { }
    
    public DbSet<Music>  Music { get; set; }
}
