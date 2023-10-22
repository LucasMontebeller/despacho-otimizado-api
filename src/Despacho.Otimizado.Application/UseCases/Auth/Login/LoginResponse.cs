namespace Despacho.Otimizado.Application.UseCases.Auth.Login
{
    public struct LoginResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}