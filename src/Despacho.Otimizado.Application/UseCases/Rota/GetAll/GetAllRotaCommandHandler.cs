using Despacho.Otimizado.Application.Results;
using Despacho.Otimizado.Domain.Interfaces;

namespace Despacho.Otimizado.Application.UseCases.Rota.GetAll
{
    public class GetAllRotaCommandHandler : BaseCommandHandler<GetAllRotaRequest>
    {
        private readonly IRotaRepository _rotaRepository;
        public GetAllRotaCommandHandler(IRotaRepository rotaRepository)
        {
            _rotaRepository = rotaRepository;
        }
        public override async Task<IResult> HandleCommand(GetAllRotaRequest request)
        {
            var response = await _rotaRepository.GetAllAsync();
            return await Task.FromResult<IResult>(response.Success());
        }
    }
}