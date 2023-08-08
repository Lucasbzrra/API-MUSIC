namespace API_MUSIC.Data.Dtos
{
    public class ReadOnlyArtist
    {


        public string Name { get; set; }

   
        public int Yearofbirth { get; set; }


        public string styleMusic { get; set; }

        public DateTime HoraDaConsulta { get; set; } = DateTime.Now;

    }
}
