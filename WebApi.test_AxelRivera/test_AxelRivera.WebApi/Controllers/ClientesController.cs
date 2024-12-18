using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using test_AxelRivera.WebApi.Features.Clientes;
using test_AxelRivera.WebApi.Features.Productos;

namespace test_AxelRivera.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteAppService _clienteAppService;
        public ClientesController(IClienteAppService clienteAppService)
        {
            _clienteAppService = clienteAppService;
        }

        [HttpGet("ObtenerClientes")]
        public IActionResult ObtenerClientes()
        {
            var result = _clienteAppService.ObtenerClientes();
            return Ok(result);
        }
    }
}
