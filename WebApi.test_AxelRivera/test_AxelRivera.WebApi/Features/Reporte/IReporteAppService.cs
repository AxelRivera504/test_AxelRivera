using test_AxelRivera.WebApi.Common;
using test_AxelRivera.WebApi.Features.Reporte.Dto;

namespace test_AxelRivera.WebApi.Features.Reporte
{
    public interface IReporteAppService
    {
        Task<Respuesta<List<ReportePeticionDto>>> ObtenerReporte(ReporteDto reporteDto);
    }
}
