using API_MUSIC.Controllers.Models;
using API_MUSIC.Data.EfCore;

namespace API_MUSIC.Services.Handlers;

public class DefaultAdminService : IAdminServices //<== Utilizando o PRINCIPIO OCP
{
    readonly IArtistDaocs _dao;
    public DefaultAdminService(IArtistDaocs dao)
    {
        _dao = dao;
    }

    public void CadastrarAtistNoBanco(Artist artist)
    {
        _dao.CadastrarNoBanco(artist);
    }

    public Artist ConsultaArtistaPeloNomeVar(string name)
    {
        return _dao.ConsultarArtistaPeloNome1(name);
    }

    public void ExcluirAristaNoBanco(Artist artist)
    {
        _dao.RemoveNoBanco(artist);
    }
    public void SalvarModificacoes()
    {
        _dao.SalvaAlteracoesFeitas();
    }
}
