using API_MUSIC.Controllers.Models;
using Microsoft.EntityFrameworkCore;

namespace API_MUSIC.Data.EfCore;

public interface IAddressDao 
{
    Address ConsultarCep(string cep);

    void Cadastrar(Address address);
    void Remover(Address address);
    void SaveChanges();
}
