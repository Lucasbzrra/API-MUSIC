using API_MUSIC.Data.EfCore;
using API_MUSIC.Models;

namespace API_MUSIC.Services.Handlers;

public class DefaultAdminProduct : IProductService
{
    private readonly IMusicDao _musicDao;
    public DefaultAdminProduct(IMusicDao musicDao )
    {
        _musicDao= musicDao;
    }
    public void CadastrarMusicaNoBanco(Music music)
    {
        _musicDao.CadastrarMusicaNoBanco(music);
    }

    public Music ProcurarMusciaPeloVar(string title)
    {
        return _musicDao.ProcurarMusciaPeloTitle1(title);
    }

    public IEnumerable<Music> ProcurarMusicas(int skip, int take)
    {
        return _musicDao.BuscarListaDeMusicas(skip, take);
    }

    public void RemoverMusic(Music music, string id)
    {
       _musicDao.RemoverMusic(music,id);
    }


    public void SalvarAlteracoesEMoficicacoes()
    {
        _musicDao.SalvarAlteracoes();
    }

}
