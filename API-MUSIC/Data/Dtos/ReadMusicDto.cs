using System.ComponentModel.DataAnnotations;

namespace API_MUSIC.Data.Dtos;

public class ReadMusicDto
{

    public double Duration { get; set; }

    public string Title { get; set; }


    public string Letter { get; set; }

    public string Compositor { get; set; }

    public DateTime HoraDaConsulta { get; set; } = DateTime.Now;
}
