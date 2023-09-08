using API_MUSIC.Controllers.Models;
using API_MUSIC.Data.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace API_MUSIC.Data.EfCore;

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
/// <summary>
/// Classe que implementa a interface IArtistDao para acessar e maipular artisttas usando Entity Framework.
/// </summary>
public class ArtistDaoComEfCore :IArtistDaocs /// <== DIP (Interface)
{
    private IMusicContext _artistContext;
    /// <summary>
    /// Contrustor que recebe um contexto de banco de dados IMusicContext para ser usado para operações de banco de dados.
    /// </summary>
    /// <param name="context"></param>
    public ArtistDaoComEfCore(IMusicContext context)
    {
        _artistContext = context;
    }
    /// <summary>
    /// Consulta artista pelo name do banco de dados
    /// </summary>
    /// <param name="name"> o nome do artista a ser consultado</param>
    /// <returns> O artista encontrado ou null se não encontrado</returns>
    public Artist ConsultarArtistaPeloNome1(string name)
    {
        var ArtistaEncontrado = _artistContext.Artists.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
        return ArtistaEncontrado;
    }
    /// <summary>
    /// Adiciona um novo artista ao banco de dados
    /// </summary>
    /// <param name="Artist"> O artista a ser cadastrado</param>
    public void CadastrarNoBanco(Artist Artist)
    {
        _artistContext.Artists.Add(Artist);
        _artistContext.SaveChanges();

    }/// <summary>
    /// Remover o artista ao banco de dados
    /// </summary>
    /// <param name="artist"> O artista a ser removido</param>
    public void RemoveNoBanco(Artist artist)
    {
        _artistContext.Artists.Remove(artist);
        _artistContext.SaveChanges();

    }
    /// <summary>
    /// Salva as alterações feitas no banco
    /// </summary>
    public void SalvaAlteracoesFeitas()
    {
        _artistContext.SaveChanges();
    }

}
