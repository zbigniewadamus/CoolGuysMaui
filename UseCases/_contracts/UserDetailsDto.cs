using Newtonsoft.Json;

namespace CoolGuys.UseCases._contracts;

public class UserDetailsDto
{
    [JsonProperty("firstname")]
    public string FirstName { get; set; }
    [JsonProperty("lastname")]
    public string LastName { get; set; }
}