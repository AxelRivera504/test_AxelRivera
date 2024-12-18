using Microsoft.EntityFrameworkCore;
using test_AxelRivera.WebApi.Common;
using test_AxelRivera.WebApi.Features.Productos;
using test_AxelRivera.WebApi.Features.Productos.Dto;
using test_AxelRivera.WebApi.Features.Reporte.Dto;
using test_AxelRivera.WebApi.Infrastructure;

namespace test_AxelRivera.WebApi.Features.Reporte
{
    public class ReporteAppService : IReporteAppService
    {
        private readonly TestAxelRiveraContext _testAxelRiveraContext;
        public ReporteAppService(TestAxelRiveraContext testAxelRiveraContext)
        {
            _testAxelRiveraContext = testAxelRiveraContext;
        }
        public async Task<Respuesta<List<ReportePeticionDto>>> ObtenerReporte(ReporteDto reporteDto)
        {
            try
            {
                var query = _testAxelRiveraContext.VwReporteDetalladoVentas
                 .Where(v => v.FechaFactura >= DateOnly.FromDateTime(reporteDto.fechaInicio.Date)
                          && v.FechaFactura <= DateOnly.FromDateTime(reporteDto.fechaFin.Date));


                if (reporteDto.clienteId > 0)
                {
                    query = query.Where(v => v.CodigoCliente == reporteDto.clienteId.Value);
                }

                if (reporteDto.productoId > 0)
                {
                    query = query.Where(v => v.ProductoId == reporteDto.productoId.Value);
                }

                var resultados = await query.ToListAsync();

                if (resultados == null || !resultados.Any())
                {
                    return Respuesta<List<ReportePeticionDto>>.Fault("No se encontraron datos para los filtros brindados.", CodigosHttp.CLIENT_ERROR_CODE);
                }

                var reporteAgrupado = resultados
                    .GroupBy(r => new { r.CodigoCliente, r.NombreCliente, r.DireccionCliente, r.TelefonoCliente, r.IdFactura, r.FechaFactura })
                    .Select(grupo => new ReportePeticionDto
                    {
                        codigoCliente = grupo.Key.CodigoCliente,
                        nombreCliente = grupo.Key.NombreCliente,
                        direccionCliente = grupo.Key.DireccionCliente,
                        telefonoCliente = grupo.Key.TelefonoCliente,
                        idFactura = grupo.Key.IdFactura,

                        fechaFactura = grupo.Key.FechaFactura,
                        reportePeticionDetalleDto = grupo.Select(d => new ReportePeticionDetalleDto
                        {
                            NombreProducto = d.NombreProducto,
                            CantidadVendida = d.CantidadVendida,
                            PrecioUnitario = d.PrecioUnitario,
                        }).ToList()
                    })
                    .ToList();

                return Respuesta<List<ReportePeticionDto>>.Success(reporteAgrupado, "Reporte generado correctamente.", "200");
            }
            catch (Exception ex)
            {
                return Respuesta<List<ReportePeticionDto>>.Fault(string.Format(Mensajes.PROCESO_ERROR, ex.Message), CodigosHttp.SERVER_ERROR_CODE, new List<ReportePeticionDto>());

            }
        }

    }
}

