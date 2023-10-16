using API_MUSIC.Controllers.Models;

namespace API_MUSIC.Data.Dtos;

public class ReadOnlyArtist
{

    public string Name { get; set; }

    public int Yearofbirth { get; set; }

    public string styleMusic { get; set; }
    public  ICollection<Music> ReadMusics { get; set; }

    public Address ReadAddress { get; set; }
    public DateTime HoraDaConsulta { get; set; } = DateTime.Now;

}
