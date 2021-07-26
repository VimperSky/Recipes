namespace Recipes.WebApi.AuthFeatures
{
    public class JwtSettings
    {
        public const string Name = "JwtSettings";
        
        public string SecurityKey { get; init; }
        
        public string ValidIssuer { get; init; }
        
        public string ValidAudience { get; init; }
        
        public int ExpiryInMinutes { get; init; }
    }
}