using test_AxelRivera.WebApi.Common;
using test_AxelRivera.WebApi.Features.Facturas.Dto;

namespace test_AxelRivera.WebApi.Features.Facturas
{
    public interface IFacturasAppService
    {
        Task<Respuesta<string>> AgregarFactura(FacturaPeticionDto facturaPeticionDtos);
    }
}
