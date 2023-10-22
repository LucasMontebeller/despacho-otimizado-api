using Despacho.Otimizado.Application.UseCases.Rota.GetAll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Despacho.Otimizado.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class RotaController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RotaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllRotaRequest());
            return result.Succeed ? Ok(result) : UnprocessableEntity(result);
        }
    }
}