using System.Text.Json.Serialization;

namespace SalutDemoConn.Models
{
    public class UserAuthInfo
    {
        [JsonPropertyName("step")]
        public string Step { get; set; } = string.Empty;

        [JsonPropertyName("client_id")]
        public string ClientId { get; set; } = string.Empty;

        [JsonPropertyName("ui_locales")]
        public string UiLocales { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("objectId")]
        public string ObjectId { get; set; } = string.Empty;

        [JsonPropertyName("surname")]
        public string SurName { get; set; } = string.Empty;

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; } = string.Empty;

        [JsonPropertyName("givenName")]
        public string GivenName { get; set; } = string.Empty;

        
        [JsonPropertyName("extension_2f81fe04720f466bb320e0a080bf6670_Hobby")]
        public string Hobby { get; set; } = string.Empty;

        
        [JsonPropertyName("extension_2f81fe04720f466bb320e0a080bf6670_Age")]
        public int Age { get; set; } = 0;
    }

}
