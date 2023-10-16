using API_MUSIC.Controllers.Models;

namespace API_MUSIC.Services;

public interface IRegistrationAdmin
{
    Address QueryAndress(string cep);
    void RegistrationAdress(Address address);
    void DeleteAndress(Address address);

    void SavaChanges();

}
