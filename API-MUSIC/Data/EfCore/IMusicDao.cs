using API_MUSIC.Controllers.Models;

namespace API_MUSIC.Data.EfCore;
/*
 * Inversão de depência criada 
 */
public interface IMusicDao
{
    IEnumerable<Music> BuscarListaDeMusicas(int skip, int take);

    Music ProcurarMusciaPeloTitle1(string title);


    void CadastrarMusicaNoBanco(Music music);

    void SalvarAlteracoes();

    void RemoverMusic(Music music);
   

}
