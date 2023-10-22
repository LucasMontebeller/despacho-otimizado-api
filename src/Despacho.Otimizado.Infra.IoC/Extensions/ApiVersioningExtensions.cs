using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Despacho.Otimizado.Infra.IoC.Extensions
{
    public static class ApiVersioningExtensions
    {
        public static IServiceCollection AddApiVersion(this IServiceCollection services)
        {
            services.AddApiVersioning(setup =>
            {
                setup.DefaultApiVersion = ApiVersion.Default;
                setup.AssumeDefaultVersionWhenUnspecified = true;
                setup.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(p =>
            {
                p.GroupNameFormat = "'v'VVV";
                p.SubstituteApiVersionInUrl = true;
            });

            return services;
        }
    }
}