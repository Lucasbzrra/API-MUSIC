using API_MUSIC.Data.EfCore;
using API_MUSIC.Models;

namespace API_MUSIC.Services.Handlers;

public class RegistrationAddress : IRegistrationAddress
{
    readonly IAddressDao _daoAdress;
    public RegistrationAddress(IAddressDao addressDao)
    {
        _daoAdress = addressDao;
    }

    public void DeleteAndress(Address address, string IdToken)
    {
        _daoAdress.Remover(address, IdToken);
    }

    public Address QueryAndress(string cep)
    {
       return _daoAdress.ConsultarCep(cep);
    }


    public void RegistrationAdress(Address address)
    {
        _daoAdress.Cadastrar(address);
    }

    public void SavaChanges()
    {
        _daoAdress.SaveChanges();
    }
}
