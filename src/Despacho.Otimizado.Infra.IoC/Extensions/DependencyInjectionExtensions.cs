using System.Reflection;
using Despacho.Otimizado.Infra.Data.Context;
using Despacho.Otimizado.Infra.Data.Entity.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Scrutor;

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

            // Services e Repositories
            services.AddScrutor(nameof(Despacho));
            
            return services;
        }

        /// <summary>
        /// Injeta automaticamente as dependências, verificando o assembly atual e registrando 
        /// todas as classes públicas e não abstratas que implementam uma interface com o mesmo nome, exceto o "I" inicial
        /// </summary>
        /// <param name="services"></param>
        /// <param name="namespace"></param>
        /// <returns></returns>
        private static IServiceCollection AddScrutor(this IServiceCollection services, string @namespace)
        {
            /* ref : https://henriquemauri.net/injecao-de-dependencia-automatica-com-o-scrutor-no-net/ */
            var assemblies = DependencyContext.Default.GetDefaultAssemblyNames().Where(assembly => assembly.FullName.StartsWith(@namespace)).Select(Assembly.Load);
            services.Scan(scan => scan.FromAssemblies(assemblies).AddClasses().UsingRegistrationStrategy(RegistrationStrategy.Skip).AsMatchingInterface().WithScopedLifetime());

            return services;
        } 
    }
}