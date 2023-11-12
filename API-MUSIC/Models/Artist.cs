using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_MUSIC.Models;

public class Artist
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdArtist { get; set; }

    [Required(ErrorMessage = "tamanho de caracteres invalido")]
    [StringLength(50)]
    public string Name { get; set; }

    [Required(ErrorMessage = " Data de nascimento invalida")]
    [Range(1908, 2022)]
    public int Yearofbirth { get; set; }



    private string _styleMusic;

    [Required(ErrorMessage = "obrigatorio colocar o estilo musical [Samba,Funk,Sertanejo,Rap]")]
    public string StyleMusic
    {
        get { return _styleMusic; }
        set
        {
            string ValueLower = value.ToLower();
            if (generos.Contains(ValueLower))
            {
                _styleMusic = ValueLower;
            }
        }
    }
    private List<string> generos = new List<string> { "samba", "funk", "sertanejo", "rap" };


    // Criando relacionamento [N]

    [JsonIgnore] ///  você pode usar a anotação [JsonIgnore] nas propriedades que você não deseja incluir na serialização. Isso pode ajudar a evitar a referência cíclica.
    public virtual ICollection<Music> ? Musics { get; set; } ///Relação [n] (relacionamento de entidade)


    // Criando relacionamento [1]
    [Required(ErrorMessage ="Campo obrigatorio")]
    public int AddressID { get; set; }
    [JsonIgnore]
    public virtual Address Address { get; set; }

    public string UserID { get; set; }
    [JsonIgnore]
    public virtual  User User { get; set; }
}
