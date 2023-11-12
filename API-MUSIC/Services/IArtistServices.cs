using API_MUSIC.Models;

namespace API_MUSIC.Services;

public interface IArtistServices //<== Utilizando o PRINCIPIO OCP
{

     Artist ConsultaArtistaPeloNomeVar(string name);
     List<Artist> RetornarMusicasDoArtist(int? id);

    Artist retornarEndereco(int id);
     void CadastrarAtistNoBanco(Artist artist);

     void SalvarModificacoes();

     void ExcluirAristaNoBanco(Artist artist, string token);
    //Music RetornandoMusicasDoArtista(string name);
}
