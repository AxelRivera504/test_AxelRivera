using test_AxelRivera.WebApi.Common;
using test_AxelRivera.WebApi.Features.Clientes.Dto;
using test_AxelRivera.WebApi.Features.Productos.Dto;

namespace test_AxelRivera.WebApi.Features.Clientes
{
    public interface IClienteAppService
    {
        Respuesta<List<ClientesDto>> ObtenerClientes();
    }
}
