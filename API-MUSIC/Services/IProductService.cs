using API_MUSIC.Models;

namespace API_MUSIC.Services;

public interface IProductService
{
    IEnumerable<Music> ProcurarMusicas(int skip, int take);
    public Music ProcurarMusciaPeloVar(string title);

    public void CadastrarMusicaNoBanco(Music music);

    public void SalvarAlteracoesEMoficicacoes();

    public void RemoverMusic(Music music, string id);

    
}
