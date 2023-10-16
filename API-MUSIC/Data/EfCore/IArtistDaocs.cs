using API_MUSIC.Controllers.Models;
using API_MUSIC.Data.Dtos;

namespace API_MUSIC.Data.EfCore;
/*
 * Inversão de depência criada 
 */ 
public interface IArtistDaocs
{
     Artist ConsultarArtistaPeloNome1(string name);

    List<Artist> RetornarMusicasDoArtist(int? id);
    void CadastrarNoBanco(Artist Artist);

     void RemoveNoBanco(Artist artist);
    Artist QueryMyAddress(int id);
    void SalvaAlteracoesFeitas();

}
