using test_AxelRivera.WebApi.Common;
using test_AxelRivera.WebApi.Features.Productos.Dto;

namespace test_AxelRivera.WebApi.Features.Productos
{
    public interface IProductosAppService
    {
        Respuesta<List<ProductoDto>> ObtenerProductos();
        Task<Respuesta<string>> AgregarProductos(ProductoDtoPeticion productoDtoPeticion);
        Task<Respuesta<string>> ActualizarProductos(ProductoDtoPeticion productoDtoPeticion);
        Task<Respuesta<string>> DesactivarProductos(ProductoDtoPeticion productoDtoPeticion);
    }
}
