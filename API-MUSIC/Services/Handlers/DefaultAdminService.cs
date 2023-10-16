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

    public Artist retornarEndereco(int id)
    {
        return  _dao.QueryMyAddress(id);
    }

    public List<Artist> RetornarMusicasDoArtist(int? id)
    {
        return _dao.RetornarMusicasDoArtist(id);
    }

    //public Music RetornandoMusicasDoArtista(string name)
    //{
    //    _dao.;
    //}

    public void SalvarModificacoes()
    {
        _dao.SalvaAlteracoesFeitas();
    }
}
