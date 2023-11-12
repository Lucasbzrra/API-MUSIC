using System.ComponentModel.DataAnnotations;

namespace API_MUSIC.Data.Dtos;

public class LoginUsersDto
{
    [Required]
    [EmailAddress(ErrorMessage ="Email invalido")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
