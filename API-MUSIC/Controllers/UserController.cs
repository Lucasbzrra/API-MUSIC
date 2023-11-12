using API_MUSIC.Data.Dtos;
using API_MUSIC.Services;
using API_MUSIC.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API_MUSIC.Controllers;

[ApiController]
[Route("/controller")]
public class UserController:ControllerBase
{
    private UsuarioService _UsuarioService;
    private IMapper _mapper;
    public UserController(UsuarioService usuarioService, IMapper mapper)
    {
        _UsuarioService = usuarioService;
        _mapper = mapper;
    }
    /// <summary>
    /// Controller que realiza o cadastramento do usuário
    /// </summary>
    /// <param name="createUserDto"> Dados necessários para realizar criação do usuário</param>
    /// <returns> usuário cadastrado </returns>
    [HttpPost("/cadastrar/user")]
    public async Task<IActionResult> Cadastrar(CreateUserDto createUserDto)
    {
        
        await _UsuarioService.register(createUserDto);
        return(Ok(createUserDto));

    }
    /// <summary>
    /// Método que realiza o login do usuário
    /// </summary>
    /// <param name="loginUsersDto"> Dados necessários para realizar o login do usuário </param>
    /// <returns></returns>
    [HttpPost("/Login/user")]
    public async Task <IActionResult> Login(LoginUsersDto loginUsersDto)
    {
        var token =await _UsuarioService.LoginUsers(loginUsersDto);
        return Ok(token);
    }
}
