using System.ComponentModel.DataAnnotations;

namespace API_MUSIC.Data.Dtos;

public class UpdateAddressDto
{
    [Required(ErrorMessage = "Tamanho máximo de 20 caracteres")]
    [StringLength(20)]
    public string Complemento { get; set; }
    private List<char> Caracteres = new List<char>();
    private string _Cep { get; set; }
    [Required]
    [MinLength(8, ErrorMessage = "Tamanho de 8 caracteres ou Cep informando e invalido")]
    [MaxLength(8, ErrorMessage = "Tamanho de 8 caracteres ou Cep informando e invalido")]

    public string Cep
    {
        get { return _Cep; }
        set
        {
            Caracteres.Clear(); //<== perdi duas horas nisto, por não prestar atenção
            for (int i = 0; i < value.Length; i++)
            {
                if (char.IsDigit(value[i]))
                {
                    Caracteres.Add(value[i]);

                }

            }
            if (Caracteres.Count == 8)
            {
                _Cep = string.Concat(Caracteres);
            }
            else
            {
                _Cep = null; // Ou outra ação, dependendo dos requisitos
            }
        }
    }

    private string _Estado { get; set; }

    [Required(ErrorMessage = "Coloque somente a sigla do Estado")]
    [MaxLength(2)]
    [MinLength(2)]
    public string Estado
    {
        get
        {
            return _Estado;
        }
        set
        {
            value = value.ToUpper();
            if (ListEstados.Contains(value))
            {
                _Estado = value;
            }

        }
    }
    private List<string> ListEstados = new List<string> { "AC", "DF", "GO", "SP", "MG" };

}
