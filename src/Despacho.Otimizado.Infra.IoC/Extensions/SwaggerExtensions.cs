using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Despacho.Otimizado.Infra.IoC.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            var version = configuration.GetValue<string>("ApiVersion");
            var name = configuration.GetValue<string>("ApiName");
            
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(version, new OpenApiInfo
                {
                    Version = version,
                    Title = name,
                    Description = "ASP.NET Core Web API do projeto Despacho Otimizado",
                    Contact = new OpenApiContact
                    {
                        Name = "Mogai",
                        Url = new Uri("https://mogai.com.br/contato")
                    }
                });
            });

            return services;
        }
    }
}