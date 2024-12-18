using Microsoft.AspNetCore.Mvc;
using test_AxelRivera.WebApi.Features.Reporte;
using test_AxelRivera.WebApi.Features.Reporte.Dto;

namespace test_AxelRivera.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteController : ControllerBase
    {
        private readonly IReporteAppService _reporteAppService;
        public ReporteController(IReporteAppService reporteAppServices)
        {
            _reporteAppService = reporteAppServices;
        }

        [HttpPost("ObtenerReporte")]
        public async Task<IActionResult> ObtenerReporte(ReporteDto reporteDto)
        {
            var result = await _reporteAppService.ObtenerReporte(reporteDto);
            return Ok(result);
        }
    }
}
