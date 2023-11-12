using API_MUSIC.Models;
using System.ComponentModel.DataAnnotations;

namespace API_MUSIC.Data.Dtos;

public class CreateAdminDto
{
    [Required]
    public string UserName { get; set; }
    [Required(ErrorMessage ="Somente números inteiros")]
    public int Mat { get; set; }

    [Required(ErrorMessage ="Coloque o id do useraspnet")]
    public string IdUser { get; set; }

}
