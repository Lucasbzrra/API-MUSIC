using API_MUSIC.Data.Dtos;
using API_MUSIC.Models;
using API_MUSIC.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_MUSIC.Controllers;


[ApiController]
[Route("Controller")]


public class AddressController:ControllerBase
{
    private UsuarioService _usuarioService;
    private IRegistrationAddress _registrationAdmin ;
    private IMapper mapper;
    public AddressController(IRegistrationAddress registrationAdmin,IMapper _mapper, UsuarioService usuarioService)
    {
            _registrationAdmin=registrationAdmin;
            mapper=_mapper;
            _usuarioService=usuarioService;
    }
    /// <summary>
    /// Cadastra um novo endereço.
    /// </summary>
    /// <param name="addressDto">Dados do endereço a ser cadastrado.</param>
    /// <returns>Retorna um objeto JSON representando o endereço recém-cadastrado.</returns>
    [Authorize]
    [HttpPost("/Cadastrar-Address")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult CadastrarAddress([FromBody] CreateAndressDto addressDto)
    {
        var EncontrarAddress = _registrationAdmin.QueryAndress(addressDto.Cep);
        if(EncontrarAddress != null) {return NotFound("Não existe este endereco com este cep cadastrado"); };
         Address address= mapper.Map<Address>(addressDto);
        _registrationAdmin.RegistrationAdress(address);
        return Ok(address);
    }

    /// <summary>
    /// Visualiza os detalhes de um endereço com base no CEP.
    /// </summary>
    /// <param name="cep">CEP do endereço a ser visualizado.</param>
    /// <returns>Retorna um objeto JSON representando os detalhes do endereço.</returns>
    [Authorize]
    [HttpGet("/Viewl-Address-{cep}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult ViewAdress(string cep)
    {
        var EncontrarAddress=_registrationAdmin.QueryAndress(cep);
        if(EncontrarAddress == null) { return NotFound("Não existe este endereco com este cep cadastrado"); }
        ReadAndressDto readAndressDto=mapper.Map<ReadAndressDto>(EncontrarAddress);
        return Ok(readAndressDto);
    }
    /// <summary>
    /// Remove um endereço com base no CEP.
    /// </summary>
    /// <param name="cep">CEP do endereço a ser removido.</param>
    /// <returns>Retorna uma resposta sem conteúdo (204 No Content).</returns>
    [Authorize(Policy="ApenasAdmin")]
    [HttpDelete("/Remover-Address-{cep}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public IActionResult DeleteAddress(string cep)
    {
        //Método Http que pegar o Bearer token do usuário
        string Token = HttpContext.Request.Headers["Authorization"];
        string  TokenDecoded =  _usuarioService.GetIdUser(Token);

        var EncontrarAddress=_registrationAdmin.QueryAndress(cep);
        if(EncontrarAddress == null) { return NotFound("Não existe este endereco com este cep cadastrado"); }
        _registrationAdmin.DeleteAndress(EncontrarAddress, TokenDecoded);
        return NoContent();
    }
    /// <summary>
    /// Atualiza todos os dados de um endereço com base no CEP.
    /// </summary>
    /// <param name="cep">CEP do endereço a ser atualizado.</param>
    /// <param name="updateAddressDto">Dados atualizados do endereço.</param>
    /// <returns>Retorna uma resposta sem conteúdo (204 No Content).</returns>
    [Authorize]
    [HttpPut("/Atualizar-All-Data-{cep}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public IActionResult AlterarAllDate(string cep,[FromBody] UpdateAddressDto updateAddressDto)
    {
        var EncontrarAdress=_registrationAdmin.QueryAndress(cep);
        if(EncontrarAdress==null) { return NotFound("Não existe este endereco com este cep cadastrado"); }
         mapper.Map(updateAddressDto,EncontrarAdress);
        _registrationAdmin.SavaChanges();
        return NoContent();
    }

}
