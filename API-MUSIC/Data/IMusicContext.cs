using API_MUSIC.Controllers.Models;
using Microsoft.EntityFrameworkCore;

namespace API_MUSIC.Data;

public class IMusicContext:DbContext
{
    public IMusicContext(DbContextOptions<IMusicContext> options):base(options) { }
    
    //protected override void OnModeCreating(ModelBuilder model)
    //{
    //    model.Entity<RecordCompany>().HasKey(recordcompanies => new { recordcompanies.ArtistId, recordcompanies.MusicId });
    //    model.Entity<RecordCompany>().HasKey(refrecordcompanies => new {refrecordcompanies})
    //}
    public DbSet<Music>  Music { get; set; }

    public DbSet<Artist> Artists { get; set; }

    public DbSet<Address> Addresse_Artists { get; set; }

    //public DbSet<RecordCompany> RecordCompanies { get; set; }
}
