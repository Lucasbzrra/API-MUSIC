using API_MUSIC.Controllers.Models;

namespace API_MUSIC.Services;

public interface IAdminServices //<== Utilizando o PRINCIPIO OCP
{

     Artist ConsultaArtistaPeloNomeVar(string name);

     void CadastrarAtistNoBanco(Artist artist);

     void SalvarModificacoes();

     void ExcluirAristaNoBanco(Artist artist);
}
