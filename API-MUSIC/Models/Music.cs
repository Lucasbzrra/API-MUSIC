using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace API_MUSIC.Models;

public class Music
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "A Duracao da musica somente pode ficar entre 1.5 até 5.5")]
    [Range(1.5, 5.5)]
    public double Duration { get; set; }

    [Required(ErrorMessage = "O titulo da musica somente pode ser até 20 caracteres")]
    [StringLength(20)]
    public string Title { get; set; }

    [Required(ErrorMessage = "Tamanho maximo de 150 Letras")]
    [StringLength(150)]
    public string Letter { get; set; }
    [Required]
    [StringLength(35)]
    public string Compositor { get; set; }


    // relação de [1] do relacionamento de entidades [1:n]

    [Required]

    public int ArtistID { get; set; }
    [JsonIgnore]
    public virtual Artist Artist { get; set; }

}
