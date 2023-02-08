using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CoolGuys.UseCases._contracts
{
    public class User
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("firstname")]
        public string? FirstName { get; set; }
        [JsonPropertyName("lastname")]
        public string? LastName { get; set; }
        [JsonPropertyName("avatar")]
        public string? avatar { get; set; }
    }
}
