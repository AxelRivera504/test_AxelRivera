using Microsoft.EntityFrameworkCore;
using test_AxelRivera.WebApi.Common;
using test_AxelRivera.WebApi.Features.Clientes.Dto;
using test_AxelRivera.WebApi.Features.Productos;
using test_AxelRivera.WebApi.Features.Productos.Dto;
using test_AxelRivera.WebApi.Infrastructure;

namespace test_AxelRivera.WebApi.Features.Clientes
{
    public class ClienteAppService : IClienteAppService
    {
        private readonly TestAxelRiveraContext _testAxelRiveraContext;
        public ClienteAppService(TestAxelRiveraContext testAxelRiveraContext)
        {
            _testAxelRiveraContext = testAxelRiveraContext;
        }
        public Respuesta<List<ClientesDto>> ObtenerClientes()
        {
            try
            {
                List<ClientesDto> clientes = (from clientesLista in _testAxelRiveraContext.Clientes.AsNoTracking()
                                               select new ClientesDto
                                               {
                                                    ClienteId = clientesLista.ClienteId,
                                                    Correo = clientesLista.Correo,
                                                    Direccion = clientesLista.Direccion ?? "N/A",
                                                    Nombre = clientesLista.Nombre ?? "N/A", 
                                                    Telefono = clientesLista.Telefono ?? "N/A",
                                               }).ToList();

                if (clientes.Count <= 0)
                    return Respuesta<List<ClientesDto>>.Fault(Mensajes.CLIENTES_OBTENIDOS_EXITOSAMENTE, CodigosHttp.SERVER_ERROR_CODE, new List<ClientesDto>());

                return Respuesta<List<ClientesDto>>.Success(clientes, Mensajes.CLIENTES_NO_OBTENIDOS_EXITOSAMENTE, CodigosHttp.SUCCESS_CODE);
            }
            catch (Exception ex)
            {
                return Respuesta<List<ClientesDto>>.Fault(string.Format(Mensajes.PROCESO_ERROR, ex.Message), CodigosHttp.SERVER_ERROR_CODE, new List<ClientesDto>());
            }
        }
    }
}
