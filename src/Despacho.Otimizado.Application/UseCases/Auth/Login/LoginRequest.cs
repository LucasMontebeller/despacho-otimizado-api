using System.ComponentModel.DataAnnotations;
using Despacho.Otimizado.Application.Results;
using MediatR;

namespace Despacho.Otimizado.Application.UseCases.Auth.Login
{
    public class LoginRequest : IRequest<IResult>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}