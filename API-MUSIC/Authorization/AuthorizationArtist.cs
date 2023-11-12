using Microsoft.AspNetCore.Authorization;


namespace API_MUSIC.Authorization;

//public class AuthorizationArtist : AuthorizationHandler<ApenasProprioArtist> <==== Deixar este trécho em STAND BY, para futuramente averiguar oq fazer
//{
//    private readonly IHttpContextAccessor httpContext;
//    public AuthorizationArtist(IHttpContextAccessor _httpContext)
//    {
//        httpContext= _httpContext;
//    }
//    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ApenasProprioArtist requirement)
//    {
//      var UseIdClaim=  httpContext.HttpContext.User.Claims.First(x => x.Type == "id").Value;
//        requirement.Id
//        //var UseIdClaim =context.User.Claims.First(x=>x.Type== "id").Value;
//        //if (UseIdClaim!=requirement.Id) { return Task.CompletedTask; }
//        //    context.Succeed(requirement);
//        //return Task.CompletedTask;
//    }
//}
