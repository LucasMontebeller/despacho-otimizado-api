using Despacho.Otimizado.Domain.Interfaces;
using Despacho.Otimizado.Infra.Data.Context;
using Despacho.Otimizado.Infra.Data.Entity.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Despacho.Otimizado.Infra.IoC.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var defaultConnection = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>
            (      
                options => options.UseSqlServer(defaultConnection,
                action => action.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
            );

            // Identity
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ ";
            });

            // Injeção dos serviços e repositories
            services.AddScoped<IUserAuthenticateService, UserAuthenticateService>();

            services.AddScrutor();
            
            return services;
        }

        // ver - https://henriquemauri.net/injecao-de-dependencia-automatica-com-o-scrutor-no-net/
        public static IServiceCollection AddScrutor(this IServiceCollection services)
        {
            return services;
        } 
    }
}