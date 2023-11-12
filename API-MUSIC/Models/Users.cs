using Microsoft.AspNetCore.Identity;

namespace API_MUSIC.Models;
//(https://learn.microsoft.com/pt-br/aspnet/core/security/authentication/customize-identity-model?view=aspnetcore-6.0)
public  class Users : IdentityUser //IdentityUser<int>  //IdentityUser IdentityRole
{
    public string CPF { get; set; }
    public DateTime DataNascimento { get; set; }
    public Users() : base() { }
}
