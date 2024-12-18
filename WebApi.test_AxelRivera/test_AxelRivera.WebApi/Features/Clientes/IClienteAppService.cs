using test_AxelRivera.WebApi.Common;
using test_AxelRivera.WebApi.Features.Clientes.Dto;

namespace test_AxelRivera.WebApi.Features.Clientes
{
    public interface IClienteAppService
    {
        Respuesta<List<ClientesDto>> ObtenerClientes();
    }
}
