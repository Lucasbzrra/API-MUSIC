using Microsoft.AspNetCore.Authorization;

namespace API_MUSIC.Authorization;

public class ApenasAdmin : IAuthorizationRequirement
{
    public ApenasAdmin(bool valid)
    {
        Valid = valid;
    }

    public bool Valid { get; set; }
}
