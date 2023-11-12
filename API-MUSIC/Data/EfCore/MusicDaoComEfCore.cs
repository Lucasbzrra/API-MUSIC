using API_MUSIC.Data.Dtos;
using API_MUSIC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net.WebSockets;

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
    private UsersDbContext _musicContext;

    /// <summary>
    /// Construtor que recebe um contexto de banco de dados IMusicContext para ser usado para operações de banco de dados.
    /// </summary>
    /// <param name="context">O contexto do banco de dados.</param>
    public MusicDaoComEfCore(UsersDbContext context)
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
        var tetse = _musicContext.MUSIC.Skip(skip).Take(take);
        return tetse;
    }
    /// <summary>
    /// Procura uma música pelo título no banco de dados.
    /// </summary>
    /// <param name="title">O título da música a ser procurada.</param>
    /// <returns>A música encontrada ou null se não encontrada.</returns>
    public Music ProcurarMusciaPeloTitle1(string title)
    {
        var EncontrarMusica = _musicContext.MUSIC.FirstOrDefault(x => x.Title.ToLower() == title.ToLower());
        return EncontrarMusica;
    }
    /// <summary>
    /// Cadastra uma nova música no banco de dados.
    /// </summary>
    /// <param name="music">A música a ser cadastrada.</param>
    public void CadastrarMusicaNoBanco(Music music)
    {
        _musicContext.MUSIC.Add(music);
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
    /// Remove uma música do banco de dados (Somente o proprio usuário que inseriu ou ADMIN).
    /// </summary>
    /// <param name="music">A música a ser removida.</param>
    public void RemoverMusic(Music music, string id)
    {
        bool EncontrarRelacaoComAdmin = _musicContext.ADMINISTRATORS.Any(x => x.IdUser.Equals(id)); ///<=== Método que retornar um true ou false se localizar o id do admin com do usuário
        var encontraIdArtist = _musicContext.ARTISTS.FirstOrDefault(x => x.UserID.Equals(id));
        if (music.ArtistID == encontraIdArtist.IdArtist || EncontrarRelacaoComAdmin == true)
        {
            _musicContext.MUSIC.Remove(music);
            SalvarAlteracoes();
        }
       throw new Exception("Este música não faz parte de suas listas de música");

    }
    
}
