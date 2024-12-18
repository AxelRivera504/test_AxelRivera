using test_AxelRivera.WebApi.Common;
using test_AxelRivera.WebApi.Features.Productos.Dto;

namespace test_AxelRivera.WebApi.Features.Productos
{
    public class ProductosDomainService
    {

        #region Validaciones Propias
        public Respuesta<bool> VerificarProductoId(int productoId)
        {
            if (productoId > 0)
                return Respuesta<bool>.Success(true);
            else
                return Respuesta<bool>.Fault("Producto no reconocido");
        }
        public Respuesta<bool> VerificarNombreProducto(string nombreProducto)
        {
            if (nombreProducto == null || nombreProducto.Trim().Length <= 0 || nombreProducto.Length > 255)
                return Respuesta<bool>.Fault("El nombre del producto es requerido y no debe ser mayor a 255 caracterres");
            else
                return Respuesta<bool>.Success(true);
        }

        public Respuesta<bool> VerificarPrecio(decimal precioProducto)
        {
            if (precioProducto <= 0)
                return Respuesta<bool>.Fault("El precio del producto no puede ser menor a cero");
            else
                return Respuesta<bool>.Success(true);
        }

        public Respuesta<bool> VerificarCantidadProducto(int CantidadDisponible)
        {
            if (CantidadDisponible < 0)
                return Respuesta<bool>.Fault("El cantidad de disponibilidad de un producto no puede ser negativo");
            else
                return Respuesta<bool>.Success(true);
        }
        #endregion

        public Respuesta<bool> ValidarInsercionProducto(ProductoDtoPeticion producto)
        {
            Respuesta<bool> validarNombreProducto = VerificarNombreProducto(producto.nombreProducto);
            if (!validarNombreProducto.Ok)
                return validarNombreProducto;

            Respuesta<bool> validarPrecioProducto = VerificarPrecio(producto.precio);
            if (!validarPrecioProducto.Ok)
                return validarPrecioProducto;

            Respuesta<bool> validarCantidadDisponible = VerificarCantidadProducto(producto.cantidadDisponible);
            if (!validarCantidadDisponible.Ok)
                return validarCantidadDisponible;

            return Respuesta<bool>.Success(true);
        }

        public Respuesta<bool> ValidarActualizacionProducto(ProductoDtoPeticion producto)
        {
            Respuesta<bool> validarProductoId = VerificarProductoId(Convert.ToInt32(producto.productoId));
            if (!validarProductoId.Ok)
                return validarProductoId;

            Respuesta<bool> validarNombreProducto = VerificarNombreProducto(producto.nombreProducto);
            if (!validarNombreProducto.Ok)
                return validarNombreProducto;

            Respuesta<bool> validarPrecioProducto = VerificarPrecio(producto.precio);
            if (!validarPrecioProducto.Ok)
                return validarPrecioProducto;

            Respuesta<bool> validarCantidadDisponible = VerificarCantidadProducto(producto.cantidadDisponible);
            if (!validarCantidadDisponible.Ok)
                return validarCantidadDisponible;

            return Respuesta<bool>.Success(true);
        }

        public Respuesta<bool> ValidarDesactivarProducto(ProductoDtoPeticion producto)
        {
            Respuesta<bool> validarProductoId = VerificarProductoId(Convert.ToInt32(producto.productoId));
            if (!validarProductoId.Ok)
                return validarProductoId;

            return Respuesta<bool>.Success(true);
        }
    }
}
