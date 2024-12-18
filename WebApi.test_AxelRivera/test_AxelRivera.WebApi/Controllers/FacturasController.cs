using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using test_AxelRivera.WebApi.Features.Facturas;
using test_AxelRivera.WebApi.Features.Facturas.Dto;
using test_AxelRivera.WebApi.Features.Productos;
using test_AxelRivera.WebApi.Features.Productos.Dto;

namespace test_AxelRivera.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        private readonly IFacturasAppService _facturasAppService;
        public FacturasController(IFacturasAppService facturasAppService)
        {
            _facturasAppService = facturasAppService;
        }

        [HttpPost("AgregarFactura")]
        public async Task<IActionResult> AgregarFactura(FacturaPeticionDto facturaPeticionDtos)
        {
            var result = await _facturasAppService.AgregarFactura(facturaPeticionDtos);
            return Ok(result);
        }
    }
}
