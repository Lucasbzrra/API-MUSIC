namespace API_MUSIC.Data.Dtos;

public class ReadAndressDto
{
    public int IdAddress { get; set; }
    public string Complemento { get; set; }

    public string Cep { get; set; }
    public string Estado { get; set; }

    public DateTime DateTime { get; set; }=DateTime.Now;
}
