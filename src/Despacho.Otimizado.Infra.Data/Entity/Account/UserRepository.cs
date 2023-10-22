using Microsoft.AspNetCore.Identity;

namespace Despacho.Otimizado.Infra.Data.Entity.Account
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User?> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IList<string>> GetRolesAsync(User user)
        {
            return await _userManager.GetRolesAsync(user);
        }
    }
}