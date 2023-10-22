using Despacho.Otimizado.Application.Results;
using MediatR;
using Serilog;

namespace Despacho.Otimizado.Application.UseCases
{
    public abstract class BaseCommandHandler<T> : IRequestHandler<T, IResult> where T : IRequest<IResult>
    {
        public abstract Task<IResult> HandleCommand(T request);

        public async Task<IResult> Handle(T request, CancellationToken cancellationToken)
        {
            try
            {
                return await HandleCommand(request);
            }
            catch (Exception ex)
            {                
                Log.Error(ex, ex.TargetSite?.DeclaringType?.Name);
                return Result<T>.Fail(default, "Erro ao processar os dados, tente novamente mais tarde.");
            }
        }
    }
}