using API_MUSIC.Controllers.Models;
using Microsoft.EntityFrameworkCore;

namespace API_MUSIC.Data.EfCore;

public class AddressDaoComEfCore:IAddressDao
{
    private IMusicContext _context;

    public AddressDaoComEfCore(IMusicContext context)
    {
        _context = context;
    }

    public void Cadastrar(Address address)
    {
        _context.Addresse_Artists.Add(address);
        _context.SaveChanges();
    }

    public Address ConsultarCep(string cep)
    {
       var CepEncontrado= _context.Addresse_Artists.FirstOrDefault(x=>x.Cep.Equals(cep));
        return CepEncontrado;
    }

  

    public void Remover(Address address)
    {
       _context.Addresse_Artists.Remove(address);
        _context.SaveChanges();
    }


    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}
