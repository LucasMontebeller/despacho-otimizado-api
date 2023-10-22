using Despacho.Otimizado.Application.Messages;
using Despacho.Otimizado.Application.Results;
using Despacho.Otimizado.Domain.Interfaces;

namespace Despacho.Otimizado.Application.UseCases.Auth.Login
{
    public class LoginCommandHandler : BaseCommandHandler<LoginRequest>
    {
        private readonly IUserAuthenticateService _userAuthenticateService;
        public LoginCommandHandler(IUserAuthenticateService userAuthenticateService)
        {
            _userAuthenticateService = userAuthenticateService;
        }
        public override async Task<IResult> HandleCommand(LoginRequest request)
        {
            var success = await _userAuthenticateService.Authenticate(request.Email, request.Password);
            if (!success)
                return Result.Fail(ErrorMessages.UserNotFound);
            
            var token = await _userAuthenticateService.CreateToken(request.Email);
            if (string.IsNullOrEmpty(token))
                return Result.Fail(ErrorMessages.CreatedTokenFailed);

            var response = new LoginResponse { Token = token, Expiration = _userAuthenticateService.GetTokenValidity() };
            return await Task.FromResult<IResult>(response.Success());
        }
    }
}