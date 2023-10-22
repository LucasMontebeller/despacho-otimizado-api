using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Despacho.Otimizado.Domain.Interfaces;
using Despacho.Otimizado.Infra.Data.Cache;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Despacho.Otimizado.Infra.Data.Entity.Account
{
    public class UserAuthenticateService : IUserAuthenticateService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly SignInManager<User> _signInManager;
        private static readonly RecordCache<User> UserCache = new(100);
        public UserAuthenticateService(
            IConfiguration configuration,
            IUserRepository userRepository, 
            SignInManager<User> signInManager
        )
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _signInManager = signInManager;
        }

        public async Task<bool> Authenticate(string email, string password)
        {
            var user = await GetUserFromCacheAsync(email);
            if (user is null)
                return false;
            
            var result = await _signInManager.PasswordSignInAsync(user.UserName, password, false, false);
            if (!result.Succeeded)
                return false;

            return true;
        }
        

        public async Task<string?> CreateToken(string email)
        {
            var user = await GetUserFromCacheAsync(email);
            if (user is null)
                return null;

            var authClaims = await GetUserClaimsAsync(user);
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            var tokenValidity = GetTokenValidity();

            var token = new JwtSecurityToken
            (
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: tokenValidity,
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public DateTime GetTokenValidity()
        {
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);
            return DateTime.Now.AddMinutes(tokenValidityInMinutes);
        }

        private async Task<List<Claim>> GetUserClaimsAsync(User user)
        {
            var authClaims = new List<Claim>
            {
                new ("email", user.Email),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var userRoles = await _userRepository.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            return authClaims;
        }

        private async Task<User?> GetUserFromCacheAsync(string email)
        {
            var user = UserCache.GetRecords().Where(x => x.Email == email).FirstOrDefault() ?? await _userRepository.FindByEmailAsync(email);
            if (user is not null && !UserCache.ContainsRecord(user))
                UserCache.Add(user);

            return user;
        }
    }
}