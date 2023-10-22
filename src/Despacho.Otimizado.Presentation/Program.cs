using Despacho.Otimizado.Infra.IoC.Extensions;
using Serilog;

namespace Despacho.Otimizado.Presentation
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

            builder.Services.AddRouting(options => options.LowercaseUrls = true);
            builder.Services.AddControllers();
            builder.Services.AddApiVersion();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddMediator();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddSecurity(builder.Configuration);
            builder.Services.AddSwagger(builder.Configuration);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });
            }

            app.UseHttpsRedirection();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}