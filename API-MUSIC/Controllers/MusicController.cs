using API_MUSIC.Data.Dtos;
using API_MUSIC.Models;
using API_MUSIC.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace API_MUSIC.Controllers;

[ApiController]
[Route("Controller")]

public class MusicController:ControllerBase
{
    private IMapper mapper;
    private IProductService ServiceProduct;
    private UsuarioService UsuarioService;
    public MusicController(IMapper _mapper, IProductService _ServiceProduct, UsuarioService usuarioService)
    {
        mapper= _mapper;
        ServiceProduct = _ServiceProduct;
        UsuarioService= usuarioService;
    }
    /// <summary>
    /// Cadastra uma musica no banco de dados
    /// </summary>
    /// <param name="music"> Obrigatorio preenchemento dos campos</param>
    /// <returns> IActionResult</returns>
    /// <response code="201"> Musica cadastrada </response> 
    [Authorize]
    [HttpPost("/Cadastrar-musicas")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult CadastrarMusica([FromBody] CreateMusicDto music)
    {
       
        var EncontrarMusica=ServiceProduct.ProcurarMusciaPeloVar(music.Title);
        if (EncontrarMusica!=null) { return BadRequest($"Musica com o titulo {music.Title}, já se encontra na nossa base de dados"); }
        Music MusicaNova = mapper.Map<Music>(music);
        ServiceProduct.CadastrarMusicaNoBanco(MusicaNova);
        return CreatedAtAction(nameof(VisualizarMusicaPeloTitulo), new { title = music.Title }, music);
    }



    /// <summary>
    /// Visualizar a musica cadastrada
    /// </summary>
    /// <param name="title"> Obrigatorio Informar o nome da musica</param>
    /// <returns> IActionResult</returns>
    /// <response code="200"> Sucesso </response>>
    [Authorize]
    [HttpGet("/Procurar-Music-{title}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult VisualizarMusicaPeloTitulo(string title)
    {
      //  var MusicaEncontrada = _daoMusic.ProcurarMusciaPeloTitle1(title);

        var MusicaEncontrada = ServiceProduct.ProcurarMusciaPeloVar(title);

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
    [Authorize]
    [HttpGet("/ListaDeMusic")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<ReadMusicDto> ListaDeMusic([FromQuery] int skip, [FromQuery] int take)
    {
        //return mapper.Map<List<ReadMusicDto>>(_daoMusic.BuscarListaDeMusicas(skip,take));
        var ListaMusicas=ServiceProduct.ProcurarMusicas(skip, take);
        return mapper.Map<List<ReadMusicDto>>(ListaMusicas).ToList();
    }
    /// <summary>
    /// Alterar todos dados da musica
    /// </summary>
    /// <param name="title"> Obrigatorio informar o nome da musica</param>
    /// <param name="musicaDto">Preenchimento de todos campos obrigatorios</param>
    /// <returns>IActionResult</returns>
    /// <response code="204"> Todos dados alterados com sucesso </response>
    [Authorize]
    [HttpPut("/Alterar-Todos-Dados-Music/{title}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult AlterarDados(string title, [FromBody] UpdateMusicDto musicaDto)
    {

        //var music = _daoMusic.ProcurarMusciaPeloTitle1(title);
        var music = ServiceProduct.ProcurarMusciaPeloVar(title);
        if (music == null) { return NotFound(); }
        else
        {
            mapper.Map<UpdateMusicDto>(music);
            ServiceProduct.SalvarAlteracoesEMoficicacoes();
           // _daoMusic.SalvarAlteracoes();
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
    [Authorize]
    [HttpPatch("/Alterar-Alguns-Dados-Music/{title}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult AlterandoAlgunsDadosArtist(string title, JsonPatchDocument<UpdateMusicDto> path)
    {
        //var MusicaEncontrada = _daoMusic.ProcurarMusciaPeloTitle1(title);
        var MusicaEncontrada = ServiceProduct.ProcurarMusciaPeloVar(title);
        if (MusicaEncontrada == null) { return BadRequest($"Musica não encontrado com titulo {title}"); }
        var MusicaAtt = mapper.Map<UpdateMusicDto>(MusicaEncontrada);
        path.ApplyTo(MusicaAtt, ModelState);
        if (!TryValidateModel(MusicaAtt))
        {
            return ValidationProblem(ModelState);
        }
        mapper.Map(MusicaAtt, MusicaEncontrada);
       // _daoMusic.SalvarAlteracoes();
        ServiceProduct.SalvarAlteracoesEMoficicacoes();
        return NoContent();

    }
    /// <summary>
    /// Exluir musica da base de dados
    /// </summary>
    /// <param name="title"> Obrigatorio informar o nome da musica</param>
    /// <returns>IActionResult</returns>
    /// <response code="204"> Musica excluída com sucesso </response>
    [Authorize(Policy = "ApenasAdmin")]
    [HttpDelete("/Excluir-Music/{title}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult ExcluirMusica(string title)
    {
        //Método Http que pegar o Bearer token do usuário
        var tokenEncpryted = HttpContext.Request.Headers["Authorization"];

        string IdUser=UsuarioService.GetIdUser(tokenEncpryted);

        var ExcluirMusica = ServiceProduct.ProcurarMusciaPeloVar(title);
        if (ExcluirMusica == null) { return BadRequest($"Musica não encontrado com o titulo {title}"); }
        ServiceProduct.RemoverMusic(ExcluirMusica,IdUser);
        return NoContent();
    }
}
