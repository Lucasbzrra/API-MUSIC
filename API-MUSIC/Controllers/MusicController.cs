using API_MUSIC.Controllers.Models;
using API_MUSIC.Data;
using API_MUSIC.Data.Dtos;
using AutoMapper;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;

namespace API_MUSIC.Controllers;
[ApiController]
[Route("controller")]
public class MusicController: ControllerBase
{
    private MusicContext _context;
    private ArtistContext _contextArtis;
    private IMapper mapper;
    public MusicController(MusicContext MusicContext,ArtistContext artistContext, IMapper _mapper)
    {
        _context= MusicContext;
        _contextArtis = artistContext;
        mapper = _mapper;
    }

    /// <summary>
    /// Adciona um artista ao banco de dados 
    /// </summary>
    /// <param name="artistDto"> Objeto com os campos necessários para criação de um filme </param>
    /// <returns>IActionResult </returns>
    /// <response code="201"> Caso a inserção seja feita com sucessp </response>
    [HttpPost("/Cadastrar-Artist")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult CadadastrarArtista([FromBody] CreateArtistDto artistDto)
    {
        bool ArtistaEncontrado1 = _contextArtis.artist.Any(x => x.Name == artistDto.Name);
        if(ArtistaEncontrado1)
        {
            return BadRequest($"Já existe este Artista {artistDto.Name} na nossa base de dados ");
        }
        Artist artista = mapper.Map<Artist>(artistDto);

        _contextArtis.artist.Add(artista);
        _contextArtis.SaveChanges();
      
        return CreatedAtAction(nameof(VisualizarArtistaPeloNome), new { name = artistDto.Name }, artista);

    }

    /// <summary>
    /// Retorna a visualização do artista 
    /// </summary>
    /// <param string="name"> Obrigatorio informar o nome do artista </param>
    /// <returns>IActionResult </returns>
    /// <response code="200"> Sucesso </response>
    [HttpGet("/VisualizarArtistaPeloNome-{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult VisualizarArtistaPeloNome( string name)
    {
        
        //bool ArtistaEncontrado1 = _context.artist.Any(x => x.Name == name);
        var ArtistaEncontrado = _contextArtis.artist.FirstOrDefault(x => x.Name == name);
        if (ArtistaEncontrado==null) 
        {
            return BadRequest($"Não existe este artista com esse {name}, na nossa base de dados");
        }
        var ArtistaDto = mapper.Map<ReadOnlyArtist>(ArtistaEncontrado);
        return Ok(ArtistaDto);

    }

    /// <summary>
    /// Alterar somente alguns dados do artista
    /// </summary>
    /// <param name="name"> Obrigatorio informar o nome do artista que deseja alterar dados</param>
    /// <param name="patch"> Os seguintes dados que deseja alterar </param>
    /// <returns>IActionResult </returns>
    /// <response code="204"> Dados Alterados Com Sucesso</response>
    [HttpPatch("/Alterar-Alguns-Dados/{name}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult AlterarAlgunsDadosDoArtista(string name, JsonPatchDocument<UpdateArtistDto> patch)
    {
        var EncontrarArtista = _contextArtis.artist.FirstOrDefault(arista => arista.Name == name);
        if (EncontrarArtista == null) { return NotFound($"Artista {name} não encontrado"); }
        var ArtistParaAtt = mapper.Map<UpdateArtistDto>(EncontrarArtista);
        patch.ApplyTo(ArtistParaAtt,ModelState);
        if (!TryValidateModel(ArtistParaAtt)) 
        {
            return ValidationProblem(ModelState);
        }
        mapper.Map(ArtistParaAtt, EncontrarArtista);
        _contextArtis.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Alterar todos dados do Artista
    /// </summary>
    /// <param name="name"> Obrigatorio informar o nome do artista para fazer alteração</param>
    /// <param name="artistDto"> Dados que serão utilizados para fazer alteração</param>
    /// <returns> IActionResult</returns>
    /// <response code="204"> Dados Alterados Com Sucesso</response>

    [HttpPut("/Alterar-Todos-Dados-Artist/{Name}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult AlterarTodosDadosDoArtist(string name, [FromBody] UpdateArtistDto artistDto)
    {
        var AchouArtista=_contextArtis.artist.FirstOrDefault(artist => artist.Name==name);
        if (AchouArtista == null) { return BadRequest($"Não existe este artista {name} na nossa base de dados"); }
        mapper.Map(artistDto, AchouArtista);
        _contextArtis.SaveChanges();
        return NoContent();

    }
    /// <summary>
    /// Excluir artista do banco de dados
    /// </summary>
    /// <param name="name"> Obrigatorio informar o nome do artista para fazer exclusão</param>
    /// <returns> IActionResult</returns>
    /// <response code="204"> Dados Excluidos Com Sucesso</response>
    [HttpDelete("/Excluir-Artist/{name}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult ExcluirArtista(string name)
    {
        var artista = _contextArtis.artist.FirstOrDefault(x => x.Name==name);
        //var artista = _contextArtis.artist.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (artista == null) { return NotFound($"Não tem a musica pelo nome {name} "); }
        _contextArtis.Remove(artista);
        _contextArtis.SaveChanges();
        return NoContent();
    }

    //MUSIC


    /// <summary>
    /// Cadastra uma musica no banco de dados
    /// </summary>
    /// <param name="music"> Obrigatorio preenchemento dos campos</param>
    /// <returns> IActionResult</returns>
    /// <response code="201"> Musica cadastrada </response> 
    [HttpPost("/Cadastrar-musicas")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult CadastrarMusica([FromBody] CreateMusicDto music)
    {

       bool EncontrarMusica= _context.Music.Any(musica =>music.Title==musica.Title);
        if(EncontrarMusica) { return BadRequest($"Musica com o titulo {music.Title}, já se encontra na nossa base de dados"); }
        Music MusicaNova= mapper.Map<Music>(music);
        _context.Music.Add(MusicaNova);
        _context.SaveChanges();
        return CreatedAtAction(nameof(VisualizarMusicaPeloTitulo), new { title = music.Title }, music);
    }


    /// <summary>
    /// Visualizar a musica cadastrada
    /// </summary>
    /// <param name="title"> Obrigatorio Informar o nome da musica</param>
    /// <returns> IActionResult</returns>
    /// <response code="200"> Sucesso </response>>
    [HttpGet("/Procurar-Music-{title}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult VisualizarMusicaPeloTitulo(string title)
    {
        var MusicaEncontrada = _context.Music.FirstOrDefault(x => x.Title == title); /* Ideia para melhorar a consulta deste codigo (envolve consulta SQL %)*/
        if (MusicaEncontrada == null)
        {
            return BadRequest($"Não existe este artista com esse {title}, na nossa base de dados");
        }
        var Visualizacao = mapper.Map<ReadMusicDto>(MusicaEncontrada);
        return Ok(Visualizacao);

    }
    /// <summary>
    /// Visualizar lista de musica
    /// </summary>
    /// <param name="skip"> Obrigatorio informar o número de página que deseja pular</param>
    /// <param name="take"> Obrigatorio informar o número de página que deseja selecionar</param>
    /// <returns>IActionResult</returns>
    /// <response code="200"> Sucesso </response>>
    [HttpGet("/ListaDeMusic")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<ReadMusicDto> ListaDeMusic([FromQuery]int skip, [FromQuery]int take)
    {
        return mapper.Map<List<ReadMusicDto>>(_context.Music.Skip(skip).Take(take));
    }
    /// <summary>
    /// Alterar todos dados da musica
    /// </summary>
    /// <param name="title"> Obrigatorio informar o nome da musica</param>
    /// <param name="musicaDto">Preenchimento de todos campos obrigatorios</param>
    /// <returns>IActionResult</returns>
    /// <response code="204"> Todos dados alterados com sucesso </response>
    [HttpPut("/Alterar-Todos-Dados-Music/{title}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult AlterarDados(string title, [FromBody] UpdateMusicDto musicaDto)
    {

        var music = _context.Music.FirstOrDefault(music => music.Title == title);
        if (music == null) { return NotFound(); }
        else
        {
            mapper.Map<UpdateMusicDto>(music);
            _context.SaveChanges();
            return NoContent();
        }
    }
    /// <summary>
    /// Alterar alguns dados da musica
    /// </summary>
    /// <param name="title"> Obrigatorio informar o nome da mmusica</param>
    /// <param name="path"> Obrigatorio informar quais dados devem ser alterado</param>
    /// <returns>IActionResult</returns>
    /// <response code="204"> Dados alterados com sucesso </response>
    [HttpPatch("/Alterar-Alguns-Dados-Music/{title}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult AlterandoAlgunsDadosArtist(string title, JsonPatchDocument<UpdateMusicDto> path)
    {
        var MusicaEncontrada = _context.Music.FirstOrDefault(musica => musica.Title == title);
        if(MusicaEncontrada==null) { return BadRequest($"Musica não encontrado com titulo {title}"); }
       var MusicaAtt=mapper.Map<UpdateMusicDto> (MusicaEncontrada);
        path.ApplyTo(MusicaAtt, ModelState);
        if (!TryValidateModel(MusicaAtt))
        {
            return ValidationProblem(ModelState);
        }
        mapper.Map(MusicaAtt, MusicaEncontrada);
        _context.SaveChanges();
        return NoContent();

    }
    /// <summary>
    /// Exluir musica da base de dados
    /// </summary>
    /// <param name="title"> Obrigatorio informar o nome da musica</param>
    /// <returns>IActionResult</returns>
    /// <response code="204"> Musica excluída com sucesso </response>
    [HttpDelete("/Excluir-Music/{title}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult ExcluirMusica(string title)
    {
        var ExluirMusica = _context.Music.FirstOrDefault(musica =>musica.Title== title);
        if(ExluirMusica == null) { return BadRequest($"Musica não encontrado com o titulo {title}"); }
        _context.Remove(ExluirMusica);
        _context.SaveChanges();
        return NoContent();
    }

    
}
