using Microsoft.EntityFrameworkCore;
using test_AxelRivera.WebApi.Features.Clientes;
using test_AxelRivera.WebApi.Features.Facturas;
using test_AxelRivera.WebApi.Features.Facturas.Dto;
using test_AxelRivera.WebApi.Features.Productos;
using test_AxelRivera.WebApi.Features.Reporte;
using test_AxelRivera.WebApi.Infrastructure;

namespace test_AxelRivera.WebApi.Common
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection service, IConfiguration configuration)
        {
            var connectionTestAxelRiveraBD = configuration.GetConnectionString("test_AxelRivera");
            service.AddDbContext<TestAxelRiveraContext>(options => options.UseSqlServer(connectionTestAxelRiveraBD)
            .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information).EnableSensitiveDataLogging());
            return service;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddScoped<TestAxelRiveraContext>()
                           .AddScoped<IProductosAppService, ProductosAppService>()
                           .AddScoped<ProductosDomainService>()
                           .AddScoped<IReporteAppService, ReporteAppService>()
                           .AddScoped<IFacturasAppService, FacturasAppService>()
                           .AddScoped<FacturaDomainService>()
                           .AddScoped<IClienteAppService, ClienteAppService>();
            
        }
    }
}
