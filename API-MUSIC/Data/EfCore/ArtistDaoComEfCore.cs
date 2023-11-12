using API_MUSIC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
public class ArtistDaoComEfCore : IArtistDaocs /// <== DIP (Interface)
{
    private UsersDbContext _artistContext;
    /// <summary>
    /// Contrustor que recebe um contexto de banco de dados IMusicContext para ser usado para operações de banco de dados.
    /// </summary>
    /// <param name="context"></param>
    public ArtistDaoComEfCore(UsersDbContext context)
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
        var ArtistaEncontrado = _artistContext.ARTISTS.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
        return ArtistaEncontrado;
    }
    /// <summary>
    /// Adiciona um novo artista ao banco de dados
    /// </summary>
    /// <param name="Artist"> O artista a ser cadastrado</param>
    public void CadastrarNoBanco(Artist Artist)
    {
        _artistContext.ARTISTS.Add(Artist);
        _artistContext.SaveChanges();

    }/// <summary>
     /// Remover o artista ao banco de dados (Somente o proprio usuário que inseriu ou ADMIN)
     /// </summary>
     /// <param name="artist"> O artista a ser removido</param>
    public void RemoveNoBanco(Artist artist, string token)
    {
        bool IsCreator = _artistContext.ARTISTS.Any(x => x.UserID.Equals(token));
        bool IsAdmin = _artistContext.ADMINISTRATORS.Any(x => x.IdUser.Equals(token));
        if (IsCreator || IsAdmin)
        {
            _artistContext.ARTISTS.Remove(artist);
            _artistContext.SaveChanges();
        }
        else
        {
            throw new Exception("O usuário não tem relação com este artist ou não e ADMIN");
        }
        

    }
    /// <summary>
    /// Salva as alterações feitas no banco
    /// </summary>
    public void SalvaAlteracoesFeitas()
    {
        _artistContext.SaveChanges();
    }

    public List<Artist> RetornarMusicasDoArtist([FromQuery] int? id)
    {

        var ListaDeMusicasDoArtist = _artistContext.ARTISTS.FromSqlRaw($"select * from artists A where A.IdArtist={id}");
        //Error está no PORFILEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE


        // var query = _artistContext.Artists.FromSqlRaw($"Select * from artists a inner join music m ON a.IdArtist=m.Id where a.IdArtist={id}"); <== Temp está forma de consultar, passando uma query direta pelo metodo

       //var query = _artistContext.Artists

       // var query = _artistContext.Artists.FromSqlRaw($"Select * from artists A where A.IdArtist={id}");

        //var query = _artistContext.Artists.AsNoTracking().Include(a => a.Musics).Where(x => x.IdArtist == id);
        
      //  return query.ToList();
        
        return ListaDeMusicasDoArtist.ToList();

    }

    public Artist QueryMyAddress( int id)
    {
        // Consulta o contexto do artista para obter informações, incluindo detalhes de endereço.
        var query =_artistContext.ARTISTS.Include(x => x.Address).FirstOrDefault(x=>x.IdArtist== id);
        // Retorna o resultado da consulta.
        return query;
    }

}