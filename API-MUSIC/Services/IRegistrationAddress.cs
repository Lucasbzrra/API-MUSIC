using API_MUSIC.Models;

namespace API_MUSIC.Services;

public interface IRegistrationAddress
{
    Address QueryAndress(string cep);
    void RegistrationAdress(Address address);
    void DeleteAndress(Address address, string IdToken);

    void SavaChanges();

}
