using System.Reflection;
using Despacho.Otimizado.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Despacho.Otimizado.Infra.IoC.Extensions
{
    public static class MediatRExtensions
    {
        public static IServiceCollection AddMediator(this IServiceCollection services)
        {
            var assemblyType = Assembly.GetAssembly(typeof(BaseCommandHandler<>)) ?? throw new NullReferenceException("");
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assemblyType));
            return services;
        }
    }
}