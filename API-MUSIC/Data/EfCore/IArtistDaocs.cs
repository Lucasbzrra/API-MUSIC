using API_MUSIC.Data.Dtos;
using API_MUSIC.Models;

namespace API_MUSIC.Data.EfCore;
/*
 * Inversão de depência criada 
 */
public interface IArtistDaocs
{
     Artist ConsultarArtistaPeloNome1(string name);

    List<Artist> RetornarMusicasDoArtist(int? id);
    void CadastrarNoBanco(Artist Artist);

     void RemoveNoBanco(Artist artist, string token);
    Artist QueryMyAddress(int id);
    void SalvaAlteracoesFeitas();

}
