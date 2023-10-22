namespace Despacho.Otimizado.Domain.Interfaces
{
    public interface IUserAuthenticateService
    {
        public Task<bool> Authenticate(string email, string password);
        public Task<string?> CreateToken(string email);
        public DateTime GetTokenValidity();
    }
}