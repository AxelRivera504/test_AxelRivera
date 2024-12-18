using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using test_AxelRivera.WebApi.Features.Productos;
using test_AxelRivera.WebApi.Features.Productos.Dto;

namespace test_AxelRivera.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductosAppService _productosAppService;
        public ProductosController(IProductosAppService productosAppService)
        {
            _productosAppService = productosAppService;
        }

        [HttpGet("ObtenerProductos")]
        public IActionResult ObtenerProductos()
        {
            var result = _productosAppService.ObtenerProductos();
            return Ok(result);
        }

        [HttpPost("AgregarProductos")]
        public async Task<IActionResult> AgregarProductos(ProductoDtoPeticion productoDtoPeticion)
        {
            var result = await _productosAppService.AgregarProductos(productoDtoPeticion);
            return Ok(result);
        }

        [HttpPut("ActualizarProductos")]
        public async Task<IActionResult> ActualizarProductos(ProductoDtoPeticion productoDtoPeticion)
        {
            var result = await _productosAppService.ActualizarProductos(productoDtoPeticion);
            return Ok(result);
        }

        [HttpPut("DesactivarProductos")]
        public async Task<IActionResult> DesactivarProductos(ProductoDtoPeticion productoDtoPeticion)
        {
            var result = await _productosAppService.DesactivarProductos(productoDtoPeticion);
            return Ok(result);
        }
    }
}
