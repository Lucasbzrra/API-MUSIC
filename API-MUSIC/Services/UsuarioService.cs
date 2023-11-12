using API_MUSIC.Data.Dtos;
using API_MUSIC.Models;
using API_MUSIC.Services.Handlers;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace API_MUSIC.Services;

public class UsuarioService
{
    private UserManager<User> _userManager;
    private SignInManager<User> _singManager;
    private TokenService _tokenService;
    private IMapper _mapper;

    public UsuarioService( UserManager<User> userManager, TokenService tokenService, SignInManager<User> signInManager,IMapper mapper)
    {
        _userManager = userManager;
        _tokenService= tokenService;
        _singManager= signInManager;
        _mapper= mapper;
    }
    /// <summary>
    /// Tarefa assincrona que realiza o cadastramento do usuário
    /// </summary>
    /// <param name="CreateuserDto"> Dados necessários para o cadastro</param>
    /// <returns> se a operação foi cadastrada com sucesso, retorna o informativo</returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task register(CreateUserDto CreateuserDto )
    {
        User user = _mapper.Map<User>(CreateuserDto);
        Console.WriteLine(CreateuserDto);
        // O método CreateAsync, realiza o cadastramento do user.
        IdentityResult result = await  _userManager.CreateAsync(user,CreateuserDto.Password);
        if(!result.Succeeded)
        {
            throw new ApplicationException("Falha ao criar");
        }
        else
        {
            Console.WriteLine("usuario cadastrado");
        }
    }
    public async Task<string> LoginUsers(LoginUsersDto loginUsersDto) //<=== Corrigir aqui 
    {
        

        var userFound = _userManager.Users.FirstOrDefault(x => x.Email.ToLower() == loginUsersDto.Email.ToLower());

        /// método que chama PasswordSingAsyn que realiza autenticação com base nas credencias fornecidas.
        var result = await _singManager.PasswordSignInAsync(userFound.UserName, loginUsersDto.Password, false, false);
        if (!result.Succeeded)
        {
            throw new ApplicationException("falha ao realizar login");
        }

        var token = _tokenService.GenerateToken(userFound);
        return token;

    }
    /// <summary>
    /// Método que consulta o id do usuário
    /// </summary>
    /// <param name="id"> Campo necessário para consultar</param>
    /// <returns> retorna o id do usuário</returns>
    public User QueryUser(string id)
    {
        var Queryuser = _userManager.Users.FirstOrDefault(x=>x.Id==id);
        return Queryuser;

    }
    public string GetIdUser(string Token)
    {
        /// Método realiza o decode do token
        string Iduser=  _tokenService.TokenDecodeIdUser(Token);
        return Iduser;
    }
}
