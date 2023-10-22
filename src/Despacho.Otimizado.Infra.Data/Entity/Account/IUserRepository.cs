namespace Despacho.Otimizado.Infra.Data.Entity.Account
{
    public interface IUserRepository
    {
        Task<User?> FindByEmailAsync(string email);
        Task<IList<string>> GetRolesAsync(User user);
    }
}