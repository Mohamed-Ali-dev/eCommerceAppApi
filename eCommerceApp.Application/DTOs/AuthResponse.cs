using System.Text.Json.Serialization;

namespace eCommerceApp.Application.DTOs
{
    public record AuthResponse
    {
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; } = false;
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }
        [JsonIgnore]
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }


    }
}
