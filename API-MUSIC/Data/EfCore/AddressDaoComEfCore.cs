using API_MUSIC.Models;

namespace API_MUSIC.Data.EfCore;

public class AddressDaoComEfCore:IAddressDao
{
    private UsersDbContext _context;
    public AddressDaoComEfCore(UsersDbContext context)
    {
        _context = context;
    }
    /// <summary>
    /// Realiza o cadastramento do endereco
    /// </summary>
    /// <param name="address"> Dados necessários para o cadastramento</param>
    public void Cadastrar(Address address)
    {
        _context.ADDRESSE_ARTISTS.Add(address);
        _context.SaveChanges();
    }
    /// <summary>
    /// Método para visualizar enderecos cadastrados no sistema
    /// </summary>
    /// <param name="cep">Dado necessário para realizar a consulta</param>
    /// <returns> retorna se encontrou o cep no sistema</returns>
    public Address ConsultarCep(string cep)
    {
       var CepEncontrado= _context.ADDRESSE_ARTISTS.FirstOrDefault(x=>x.Cep.Equals(cep));
        return CepEncontrado;
    }


    /// <summary>
    /// Método do Ef que realizar remoção do endereco (Somente o proprio usuário que inseriu ou ADMIN)
    /// </summary>
    /// <param name="address"> Dados do endereco </param>
    /// <param name="idToken"> Dados do token do usuário</param>
    /// <exception cref="Exception"></exception>
    public void Remover(Address address, string idToken)
    {
        bool EncontrarRelacaoComAdmin = _context.ADMINISTRATORS.Any(x => x.IdUser.Equals(idToken));
        if (EncontrarRelacaoComAdmin) 
        {
            _context.ADDRESSE_ARTISTS.Remove(address);
            _context.SaveChanges();
        }
        if(!EncontrarRelacaoComAdmin)
        {
            var idCreator = _context.ARTISTS.FirstOrDefault(x => x.UserID.Equals(idToken));
            bool encontrouCreator = _context.ADDRESSE_ARTISTS.Any(x => x.IdAddress == idCreator.AddressID);
            if (encontrouCreator)
            {
                _context.ADDRESSE_ARTISTS.Remove(address);
                _context.SaveChanges();
            }
        }
        else
        {
            throw new Exception("O usuário não tem relação com este endereco ou não e ADMIN");
        }

    }


    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}
