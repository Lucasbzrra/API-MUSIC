using API_MUSIC.Controllers.Models;
using Microsoft.EntityFrameworkCore;

namespace API_MUSIC.Data;

public class ArtistContext:DbContext
{
	public ArtistContext(DbContextOptions<ArtistContext> options) : base(options) { }
	
	public DbSet<Artist> artist { get; set; }
	
}
