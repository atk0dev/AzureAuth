using System.Text.Json.Serialization;

namespace SalutDemoConn.Models
{
    public class UserCreateInfoIdentitie
    {
        [JsonPropertyName("signInType")]
        public string SignInType { get; set; } = string.Empty;

        [JsonPropertyName("issuer")]
        public string Issuer { get; set; } = string.Empty;

        [JsonPropertyName("issuerAssignedId")]
        public string IssuerAssignedId { get; set; } = string.Empty;
    }
}
