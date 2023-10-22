using Despacho.Otimizado.Application.Results;
using MediatR;

namespace Despacho.Otimizado.Application.UseCases
{
    public abstract class BaseCommandHandler<T> : IRequestHandler<T, IResult> where T : IRequest<IResult>
    {
        public abstract Task<IResult> HandleCommand(T request);

        public async Task<IResult> Handle(T request, CancellationToken cancellationToken)
        {
            try
            {
                
            }
            catch (Exception ex)
            {                
            }
            return await HandleCommand(request);
        }
    }
}