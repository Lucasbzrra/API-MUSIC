using API_MUSIC.Controllers.Models;
using API_MUSIC.Data.EfCore;

namespace API_MUSIC.Services.Handlers;

public class RegistrationAdmin : IRegistrationAdmin
{
    readonly IAddressDao _daoAdress;
    public RegistrationAdmin(IAddressDao addressDao)
    {
        _daoAdress = addressDao;
    }
    public void DeleteAndress(Address address)
    {
        _daoAdress.Remover(address);
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
