using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace API_MUSIC.Authorization;

public class AuthorizationAdmin : AuthorizationHandler<ApenasAdmin>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ApenasAdmin requirement)
    {

        //Faz uma busca pelo tipo da claim do e-mail
        var ContainEmail = context.User.Claims.FirstOrDefault(claim => claim.Type ==ClaimTypes.Email);
        if (ContainEmail is null) { return Task.CompletedTask; }
        context.Succeed(requirement);
        return Task.CompletedTask;
      

    }
}
