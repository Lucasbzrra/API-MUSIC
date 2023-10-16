using API_MUSIC.Controllers.Models;
using API_MUSIC.Data.Dtos;
using API_MUSIC.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API_MUSIC.Controllers;


[ApiController]
[Route("Controller")]


public class AddressController:ControllerBase
{
    private IRegistrationAdmin _registrationAdmin ;
    private IMapper mapper;
    public AddressController(IRegistrationAdmin registrationAdmin,IMapper _mapper)
    {
            _registrationAdmin=registrationAdmin;
            mapper=_mapper;
    }

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
    [HttpGet("/Viewl-Address-{cep}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult ViewAdress(string cep)
    {
        var EncontrarAddress=_registrationAdmin.QueryAndress(cep);
        if(EncontrarAddress == null) { return NotFound("Não existe este endereco com este cep cadastrado"); }
        ReadAndressDto readAndressDto=mapper.Map<ReadAndressDto>(EncontrarAddress);
        return Ok(readAndressDto);
    }
    [HttpDelete("/Remover-Address-{cep}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public IActionResult DeleteAddress(string cep)
    {
        var EncontrarAddress=_registrationAdmin.QueryAndress(cep);
        if(EncontrarAddress == null) { return NotFound("Não existe este endereco com este cep cadastrado"); }
        _registrationAdmin.DeleteAndress(EncontrarAddress);
        return NoContent();
    }

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
