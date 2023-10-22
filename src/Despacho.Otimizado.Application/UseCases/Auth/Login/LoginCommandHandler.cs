using Despacho.Otimizado.Application.Results;

namespace Despacho.Otimizado.Application.UseCases.Auth.Login
{
    public class LoginCommandHandler : BaseCommandHandler<LoginRequest>
    {
        public override Task<IResult> HandleCommand(LoginRequest request)
        {
            var response = new LoginResponse { Id = 2 };
            return Task.FromResult<IResult>(response.Success());
        }
    }
}