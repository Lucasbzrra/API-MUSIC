using System.Text.Json.Serialization;

namespace API_MUSIC.Models;

public class User : Users
{

    public User() : base() { }
    [JsonIgnore]
    public virtual Administrator? Administrator { get; set; }

    [JsonIgnore]
    public virtual Artist? Artist { get; set; }

}
