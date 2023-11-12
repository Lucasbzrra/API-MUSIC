using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_MUSIC.Models;

public class Administrator
{
    [Key]
    [Required]
    public int IdAdmin { get; set; }

    public string UserName { get; set; }

    public int Mat { get; set; }


    //[Required(ErrorMessage ="Coloque o id do usuario da tabela aspnetusers")]
    public string IdUser { get; set; }
    [JsonIgnore]
    public virtual User User { get; set; }

}
