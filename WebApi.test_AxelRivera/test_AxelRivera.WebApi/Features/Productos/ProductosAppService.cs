using Microsoft.EntityFrameworkCore;
using test_AxelRivera.WebApi.Common;
using test_AxelRivera.WebApi.Features.Productos.Dto;
using test_AxelRivera.WebApi.Infrastructure;
using test_AxelRivera.WebApi.Infrastructure.Enitites;

namespace test_AxelRivera.WebApi.Features.Productos
{
    public class ProductosAppService : IProductosAppService
    {
        private readonly TestAxelRiveraContext _testAxelRiveraContext;
        private readonly ProductosDomainService _productosDomainService;
        public ProductosAppService(TestAxelRiveraContext testAxelRiveraContext, ProductosDomainService productosDomainService)
        {
            _testAxelRiveraContext = testAxelRiveraContext;
            _productosDomainService = productosDomainService;
        }
        public Respuesta<List<ProductoDto>> ObtenerProductos()
        {
            try
            {
                List<ProductoDto> productos = (from productolista in _testAxelRiveraContext.Productos.AsNoTracking()
                                               where productolista.CantidadDisponible > 0
                                               select new ProductoDto { 
                                                CantidadDisponible = productolista.CantidadDisponible,
                                                Descripcion = productolista.Descripcion,
                                                NombreProducto = productolista.NombreProducto,
                                                Precio = productolista.Precio,  
                                                ProductoId = productolista.ProductoId,
                                                Descontinuado = productolista.Descontinuado ?? false
                                               }).ToList();

                if(productos.Count <= 0)
                    return Respuesta<List<ProductoDto>>.Fault(Mensajes.PRODUCTOS_NO_OBTENIDOS_EXITOSAMENTE, CodigosHttp.SERVER_ERROR_CODE, new List<ProductoDto>());

                return Respuesta<List<ProductoDto>>.Success(productos, Mensajes.PRODUCTOS_OBTENIDOS_EXITOSAMENTE,CodigosHttp.SUCCESS_CODE);
            }
            catch (Exception ex)
            {
                return Respuesta<List<ProductoDto>>.Fault(string.Format(Mensajes.PROCESO_ERROR, ex.Message), CodigosHttp.SERVER_ERROR_CODE, new List<ProductoDto>());
            }
        }

        public async Task<Respuesta<string>> AgregarProductos(ProductoDtoPeticion productoDtoPeticion)
        {
            try
            {
                Respuesta<bool> repuesta = _productosDomainService.ValidarInsercionProducto(productoDtoPeticion);
                if (!repuesta.Ok)
                    return Respuesta<string>.Fault(repuesta.Mensaje,CodigosHttp.SERVER_ERROR_CODE);

                Producto producto = new Producto()
                {
                    CantidadDisponible = productoDtoPeticion.cantidadDisponible,
                    Precio = productoDtoPeticion.precio,
                    Descripcion = productoDtoPeticion.descripcion,
                    NombreProducto = productoDtoPeticion.nombreProducto,
                    Descontinuado = productoDtoPeticion.descontinuado
                };

                await _testAxelRiveraContext.Productos.AddAsync(producto);
                await _testAxelRiveraContext.SaveChangesAsync();

                return Respuesta<string>.Success("", Mensajes.PRODUCTO_ACTUALIZADO_EXITOSAMENTE, CodigosHttp.SUCCESS_CODE);
            }
            catch (Exception ex)
            {
                return Respuesta<string>.Fault(string.Format(Mensajes.PROCESO_ERROR, ex.Message), CodigosHttp.SERVER_ERROR_CODE);
            }
        }

        public async Task<Respuesta<string>> ActualizarProductos(ProductoDtoPeticion productoDtoPeticion)
        {
            try
            {
                Respuesta<bool> repuesta = _productosDomainService.ValidarActualizacionProducto(productoDtoPeticion);
                if (!repuesta.Ok)
                    return Respuesta<string>.Fault(repuesta.Mensaje, CodigosHttp.SERVER_ERROR_CODE);

                Producto? productoABuscar = await _testAxelRiveraContext.Productos.FirstOrDefaultAsync(x => x.ProductoId == productoDtoPeticion.productoId);

                if (productoABuscar is null)
                    return Respuesta<string>.Fault(Mensajes.PRODUCTO_ACTUALIZAR_NO_ENCONTRADO, CodigosHttp.SERVER_ERROR_CODE);

                productoABuscar.Descripcion = productoDtoPeticion.descripcion;
                productoABuscar.CantidadDisponible = productoDtoPeticion.cantidadDisponible;
                productoABuscar.NombreProducto = productoDtoPeticion.nombreProducto;
                productoABuscar.Precio = productoDtoPeticion.precio;

                await _testAxelRiveraContext.SaveChangesAsync();

                return Respuesta<string>.Success("",Mensajes.PRODUCTO_ACTUALIZADO_EXITOSAMENTE, CodigosHttp.SUCCESS_CODE);
            }
            catch (Exception ex)
            {
                return Respuesta<string>.Fault(string.Format(Mensajes.PROCESO_ERROR, ex.Message), CodigosHttp.SERVER_ERROR_CODE);
            }
        }

        public async Task<Respuesta<string>> DesactivarProductos(ProductoDtoPeticion productoDtoPeticion)
        {
            try
            {
                Respuesta<bool> repuesta = _productosDomainService.ValidarDesactivarProducto(productoDtoPeticion);
                if (!repuesta.Ok)
                    return Respuesta<string>.Fault(repuesta.Mensaje, CodigosHttp.SERVER_ERROR_CODE);

                Producto? productoABuscar = await _testAxelRiveraContext.Productos.FirstOrDefaultAsync(x => x.ProductoId == productoDtoPeticion.productoId);

                if (productoABuscar is null)
                    return Respuesta<string>.Fault(Mensajes.PRODUCTO_DESCATIVAR_NO_ENCONTRADO, CodigosHttp.SERVER_ERROR_CODE);

                productoABuscar.Descontinuado = productoDtoPeticion.descontinuado;
                await _testAxelRiveraContext.SaveChangesAsync();

                return Respuesta<string>.Success("", Mensajes.PRODUCTO_DESCATIVAR_EXITOSAMENTE, CodigosHttp.SUCCESS_CODE);
            }
            catch (Exception ex)
            {
                return Respuesta<string>.Fault(string.Format(Mensajes.PROCESO_ERROR, ex.Message), CodigosHttp.SERVER_ERROR_CODE);
            }
        }

    }
}
