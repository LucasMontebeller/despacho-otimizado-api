using Despacho.Otimizado.Application.UseCases.Auth.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Despacho.Otimizado.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Post([FromBody] LoginRequest request)
    {
        var result = await _mediator.Send(request);
        return result.Succeed ? Ok(result) : UnprocessableEntity(result);
    }

}