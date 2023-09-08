using API_MUSIC.Controllers.Models;

namespace API_MUSIC.Data.EfCore;

public class MusicDaoComEfCore : IMusicDao /// <== DIP (Interface)

{
    /*
	* DAO (Data Acesss Object)
	* O Objetivo:
	* Promover independência da aplicação da forma com que os dados são permitidos
	* 
	* Motivo para isto: 
	* Permite a flexibilidade na troca da fonte de dados 
	* Transparência de uso de dados para aplicação
	* 
	*/
    private IMusicContext _musicContext;

    /// <summary>
    /// Construtor que recebe um contexto de banco de dados IMusicContext para ser usado para operações de banco de dados.
    /// </summary>
    /// <param name="context">O contexto do banco de dados.</param>
    public MusicDaoComEfCore(IMusicContext context)
    {
        _musicContext = context;

    }
    /// <summary>
    /// Busca uma lista de músicas com paginação.
    /// </summary>
    /// <param name="skip">O número de registros a serem ignorados.</param>
    /// <param name="take">O número de registros a serem retornados.</param>
    /// <returns>Uma coleção de músicas.</returns>
    public IEnumerable<Music> BuscarListaDeMusicas(int skip, int take)
    {
        var tetse = _musicContext.Music.Skip(skip).Take(take);
        return tetse;
    }
    /// <summary>
    /// Procura uma música pelo título no banco de dados.
    /// </summary>
    /// <param name="title">O título da música a ser procurada.</param>
    /// <returns>A música encontrada ou null se não encontrada.</returns>
    public Music ProcurarMusciaPeloTitle1(string title)
    {
        var EncontrarMusica = _musicContext.Music.FirstOrDefault(x => x.Title.ToLower() == title.ToLower());
        return EncontrarMusica;
    }
    /// <summary>
    /// Cadastra uma nova música no banco de dados.
    /// </summary>
    /// <param name="music">A música a ser cadastrada.</param>
    public void CadastrarMusicaNoBanco(Music music)
    {
        _musicContext.Music.Add(music);
        SalvarAlteracoes();
    }
    /// <summary>
    /// Salva todas as alterações feitas no contexto do banco de dados.
    /// </summary>
    public void SalvarAlteracoes()
    {
        _musicContext.SaveChanges();
    }

    /// <summary>
    /// Remove uma música do banco de dados.
    /// </summary>
    /// <param name="music">A música a ser removida.</param>
    public void RemoverMusic(Music music)
    {
        _musicContext.Music.Remove(music);
        SalvarAlteracoes();
    }
}
