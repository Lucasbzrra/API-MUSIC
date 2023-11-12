using System.ComponentModel.DataAnnotations;

namespace API_MUSIC.Data.Dtos;

public class CreateUserDto
{

    [Required]
    public string CPF { get; set; }
    [Required(ErrorMessage = "Necessario até 11 caracteres com letras, caracteres especias e números ")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required(ErrorMessage ="Necessario até 11 caracteres com letras, caracteres especias e números ")]
    [DataType(DataType.Password)]
    public string Repassword { get; set; }
    private DateTime _DataNascimento { get; set; }
    [Required(ErrorMessage = "Somente maiores de idades permitos")]
    public DateTime DataNascimento
    {
        get { return _DataNascimento; }
        set
        {
            if (DateTime.Now.Year == value.Year)

                throw new ArgumentException("FALHA");
            else
            {
                _DataNascimento = value;
            }
        }
    }

    [Required]
    [EmailAddress(ErrorMessage = "Email invalido")]
    public string Email { get; set; }

    [Required]
    public string UserName { get; set; }

}
