using API_MUSIC.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_MUSIC.Services.Handlers;

public class TokenService
{
    private IConfiguration _configuration; //<== Utilizar futuramente para "esconder a chave de segurança na maquina e não na aplicação"

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    /// <summary>
    /// Método que realiza a criaçãod o token
    /// </summary>
    /// <param name="user"> dados necessários para criação</param>
    /// <returns> retornar o token (cripitografado) do usuário </returns>
    public string GenerateToken(User user)
    {
        Claim[] claims = new Claim[]
        {
            new Claim("email",user.Email),
            new Claim("id",user.Id),
            new Claim(ClaimTypes.DateOfBirth,user.DataNascimento.ToString()),
            new Claim("Proprio",ClaimTypes.NameIdentifier),
            new Claim("CPF",user.CPF),
            new Claim("loginTimestamp",DateTime.UtcNow.ToString())
        };
        var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sjdmakaaaasdlapkdaopkdiajnfmnacswa"));

        var signCredential= new SigningCredentials(chave,SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken
            (
                expires: DateTime.Now.AddMinutes(10),
                claims: claims,
                signingCredentials: signCredential
            );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    /// <summary>
    /// Método que descripitografa o token do usuário
    /// </summary>
    /// <param name="token"> passa o dado JWT token Bearer</param>
    /// <returns> retorna a id do claim do token </returns>
    /// <exception cref="Exception"></exception>
    public string TokenDecodeIdUser(string token)
    {
        if(token !=null)
        {
            var jwtEnconding = token.Substring(7); // Cortar a parte do "bearer"
            var tokef = new JwtSecurityToken(jwtEnconding);// Método que realiza o decode
            string IdUser= tokef.Claims.First(c => c.Type == "id").Value;

            return IdUser;
        }
        throw new Exception("Token invalido");
       
        
    }
}
