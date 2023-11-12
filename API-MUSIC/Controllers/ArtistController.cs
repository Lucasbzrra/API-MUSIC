using API_MUSIC.Data.Dtos;
using API_MUSIC.Models;
using API_MUSIC.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace API_MUSIC.Controllers;
[ApiController]
[Route("controller")]
public class ArtistController: ControllerBase
{
    //private IArtistDaocs _daoArtist; <== Após utilizar o principio, não será mais necessário 
    private IArtistServices Service; //<== Utilizando o PRINCIPIO OCP
    private IMapper mapper;
    private IProductService Product;
    private UsuarioService UsuarioService;

    public ArtistController(IMapper _mapper, IArtistServices service,IProductService  product, UsuarioService usuarioService)
    {
        mapper = _mapper;
        Service = service;
        Product = product;
        UsuarioService = usuarioService;

    }

    /// <summary>
    /// Adciona um artista ao banco de dados 
    /// </summary>
    /// <param name="artistDto"> Objeto com os campos necessários para criação de um filme </param>
    /// <returns>IActionResult </returns>
    /// <response code="201"> Caso a inserção seja feita com sucessp </response>
    [Authorize]
    [HttpPost("/Cadastrar-Artist")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult CadadastrarArtista([FromBody] CreateArtistDto artistDto)
    {
        //bool ArtistaEncontrado = _daoArtist.ConsultarArtistaPeloNome(artistDto.Name); 
        var ArtistaEncontrado = Service.ConsultaArtistaPeloNomeVar(artistDto.Name); //<== Utilizando o PRINCIPIO OCP
       

        if (ArtistaEncontrado!=null)
        {
            return BadRequest($"Já existe este Artista {artistDto.Name} na nossa base de dados ");
        }
        Artist artista = mapper.Map<Artist>(artistDto);

        Service.CadastrarAtistNoBanco(artista);// <== Utilizando o PRINCIPIO OCP

        return CreatedAtAction(nameof(VisualizarArtistaPeloNome), new { name = artistDto.Name }, artista);

    }

    /// <summary>
    /// Retorna a visualização do artista 
    /// </summary>
    /// <param string="name"> Obrigatorio informar o nome do artista </param>
    /// <returns>IActionResult </returns>
    /// <response code="200"> Sucesso </response>
    [Authorize]
    [HttpGet("/VisualizarArtistaPeloNome-{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult VisualizarArtistaPeloNome(string name)
    {

        //var ArtistaEncontrado = _daoArtist.ConsultarArtistaPeloNome1(name);
        var ArtistaEncontrado = Service.ConsultaArtistaPeloNomeVar(name); // <== Utilizando o PRINCIPIO OCP

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
    [Authorize]
    [HttpPatch("/Alterar-Alguns-Dados/{name}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult AlterarAlgunsDadosDoArtista(string name, JsonPatchDocument<UpdateArtistDto> patch)
    {
        //var EncontrarArtista = _daoArtist.ConsultarArtistaPeloNome1(name);  

        var EncontrarArtista = Service.ConsultaArtistaPeloNomeVar(name); //<== Utilizando o PRINCIPIO OCP

        if (EncontrarArtista == null) { return NotFound($"Artista {name} não encontrado"); }

        var ArtistParaAtt = mapper.Map<UpdateArtistDto>(EncontrarArtista);

        patch.ApplyTo(ArtistParaAtt, ModelState);

        if (!TryValidateModel(ArtistParaAtt))
        {
            return ValidationProblem(ModelState);
        }
        mapper.Map(ArtistParaAtt, EncontrarArtista);
        Service.SalvarModificacoes();
        return NoContent();
    }

    /// <summary>
    /// Alterar todos dados do Artista
    /// </summary>
    /// <param name="name"> Obrigatorio informar o nome do artista para fazer alteração</param>
    /// <param name="artistDto"> Dados que serão utilizados para fazer alteração</param>
    /// <returns> IActionResult</returns>
    /// <response code="204"> Dados Alterados Com Sucesso</response>
    [Authorize]
    [HttpPut("/Alterar-Todos-Dados-Artist/{Name}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult AlterarTodosDadosDoArtist(string name, [FromBody] UpdateArtistDto artistDto)
    {
        //var AchouArtista = _daoArtist.ConsultarArtistaPeloNome1(name); Quando não era utilizado o principio OCP

        var AchouArtista = Service.ConsultaArtistaPeloNomeVar(name); //<== Utilizando o PRINCIPIO OCP

        if (AchouArtista == null) { return BadRequest($"Não existe este artista {name} na nossa base de dados"); }
        
        mapper.Map(artistDto, AchouArtista);

        // _daoArtist.SalvaAlteracoesFeitas(); Quando não era utilizado o principio OCP
        Service.SalvarModificacoes(); //<== Utilizando o PRINCIPIO OCP

        return NoContent();

    }
    /// <summary>
    /// Excluir artista do banco de dados
    /// </summary>
    /// <param name="name"> Obrigatorio informar o nome do artista para fazer exclusão</param>
    /// <returns> IActionResult</returns>
    /// <response code="204"> Dados Excluidos Com Sucesso</response>
    [Authorize(Policy = "ApenasAdmin")]
    [HttpDelete("/Excluir-Artist/{name}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult ExcluirArtista(string name)
    {
        //Método Http que pegar o Bearer token do usuário
        var tokenCod = HttpContext.Request.Headers["Authorization"];

       string IdUser= UsuarioService.GetIdUser(tokenCod);

        // var artista = _daoArtist.ConsultarArtistaPeloNome1(name); Quando não era utilizado o principio OCP

        var artista = Service.ConsultaArtistaPeloNomeVar(name); //<== Utilizando o PRINCIPIO OCP

        //var artista = _contextArtis.artist.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (artista == null) { return NotFound($"Não tem a musica pelo nome {name} "); }


        // _daoArtist.RemoveNoBanco(artista); Quando não era utilizado o principio OCP

        Service.ExcluirAristaNoBanco(artista, IdUser);  //<== Utilizando o PRINCIPIO OCP

        return NoContent();
    }

    [Authorize]
    [HttpGet("/VisualizarLista/{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<ReadOnlyArtist> Teste( string name)
    {
        var artista = Service.ConsultaArtistaPeloNomeVar(name);
        if (artista == null) 
        {
           
            return Enumerable.Empty<ReadOnlyArtist>(); 
        }
        
        var ListaDeMusicasDoArtist = Service.RetornarMusicasDoArtist(artista.IdArtist);

        var result = mapper.Map<List<ReadOnlyArtist>>(ListaDeMusicasDoArtist);
       
        return result;
    }
    [Authorize]
    [HttpGet("/VisualizarDadosCadastrais/{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult VisulizarEndereco(string name)
    {
        var EncontrarPeloName = Service.ConsultaArtistaPeloNomeVar(name);
        if (EncontrarPeloName == null) { return NotFound($"Não existe este artista com esse nome ({name})"); }
        var EncontrarAdress = Service.retornarEndereco(EncontrarPeloName.IdArtist);
        ReadOnlyArtist artist =mapper.Map<ReadOnlyArtist>(EncontrarAdress);
         return Ok(artist);
        
    }

}


