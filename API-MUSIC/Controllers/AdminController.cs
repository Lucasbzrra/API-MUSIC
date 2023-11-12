using API_MUSIC.Data.Dtos;
using API_MUSIC.Models;
using API_MUSIC.Services;
using API_MUSIC.Services.Handlers;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_MUSIC.Controllers;
[ApiController]
[Route("/Controller")]
public class AdminController : ControllerBase
{
    private IMapper _mapper;
    private IAdminService _adminService;
    private UsuarioService _usuarioService;
    private TokenService TokenService;

    public AdminController(IMapper mapper,IAdminService adminService, UsuarioService usuarioService, TokenService token)
    {
        _mapper = mapper;
        _adminService = adminService;
        _usuarioService = usuarioService;
        TokenService= token;

    }
    [HttpPost("/Cadastroadmin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize(Policy = "ApenasAdmin")]
    public IActionResult Criar(CreateAdminDto create)
    {
        Administrator administrator = _mapper.Map<Administrator>(create);
        var usuarioLocalizado = _usuarioService.QueryUser(administrator.IdUser);
        if(usuarioLocalizado!=null)
        {
            var AdminLocalizado=_adminService.ConsultAdministratorIdUser(administrator.IdUser);
            if(AdminLocalizado is null)
            {
                _adminService.Cadastrar(administrator);
                _adminService.Save();
                return Ok("cadastro Criado");
            }
            else
            {
                return NotFound($"O este admin {administrator.UserName}, já consta na nossa base de dados");
            }
        }
        else
        {
            return NotFound("O id fornecido, não corresponde ao de algum usuário");
        }
        
    }
    [HttpDelete("/Removeradmin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Authorize(Policy = "ApenasAdmin")]
    public IActionResult Remover([FromQuery] int mat)
    {
        var AdminLocalizado = _adminService.AdministratorMat(mat);
        if(AdminLocalizado is null) { return NotFound($"matricula não encontrada {mat}"); }
        _adminService.DeleteAdminstrator(AdminLocalizado);
        _adminService.Save();
        return NoContent();
    }
    [Authorize(Policy ="ApenasAdmin")]
    [HttpGet("/Visualizaradmins")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<Administrator> VisualizarAdm()
    {
        
        return _adminService.RetornarAllAdmin();
    }
}
