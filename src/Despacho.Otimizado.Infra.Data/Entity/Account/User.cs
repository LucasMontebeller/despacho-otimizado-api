using Microsoft.AspNetCore.Identity;

namespace Despacho.Otimizado.Infra.Data.Entity.Account
{
    public class User : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime AccessTokenExpirationTime { get; set; }
    }
}