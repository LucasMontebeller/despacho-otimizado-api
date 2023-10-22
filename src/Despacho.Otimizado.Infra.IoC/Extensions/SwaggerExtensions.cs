using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Despacho.Otimizado.Infra.IoC.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            var version = "v1" ;// ApiVersion.Default.ToString();
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
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            return services;
        }
    }
}