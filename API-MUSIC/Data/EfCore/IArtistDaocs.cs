using API_MUSIC.Controllers.Models;

namespace API_MUSIC.Data.EfCore;
/*
 * Inversão de depência criada 
 */ 
public interface IArtistDaocs
{
     Artist ConsultarArtistaPeloNome1(string name);

     void CadastrarNoBanco(Artist Artist);

     void RemoveNoBanco(Artist artist);

     void SalvaAlteracoesFeitas();

}
