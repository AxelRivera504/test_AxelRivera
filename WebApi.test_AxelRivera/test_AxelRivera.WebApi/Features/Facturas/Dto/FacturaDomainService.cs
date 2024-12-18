using test_AxelRivera.WebApi.Common;
using test_AxelRivera.WebApi.Features.Productos.Dto;
using test_AxelRivera.WebApi.Infrastructure.Enitites;

namespace test_AxelRivera.WebApi.Features.Facturas.Dto
{
    public class FacturaDomainService
    {
        #region Validaciones Propias
        public Respuesta<bool> VerificarExistenciaProducto(int productoId, List<Producto> productos)
        {
            var existeProducto = productos.Any(p => p.ProductoId == productoId);
            return existeProducto
                ? Respuesta<bool>.Success(true)
                : Respuesta<bool>.Fault("El producto con el ID proporcionado no existe.", "404");
        }
        public Respuesta<bool> VerificarExistenciaCliente(int clienteId, List<Cliente> clientes)
        {
            var existeCliente = clientes.Any(c => c.ClienteId == clienteId);
            return existeCliente
                ? Respuesta<bool>.Success(true)
                : Respuesta<bool>.Fault("El cliente con el ID proporcionado no existe.", "404");
        }
        public Respuesta<bool> ValidarCantidadDisponibleProducto(Producto producto, int cantidadSolicitada)
        {
            return producto.CantidadDisponible >= cantidadSolicitada
                ? Respuesta<bool>.Success(true)
                : Respuesta<bool>.Fault($"La cantidad solicitada del producto '{producto.NombreProducto}' supera la cantidad disponible.", "400");
        }
        public Respuesta<bool> ValidarDatosFactura(FacturaPeticionDto factura)
        {
            if (factura.codigoCliente <= 0)
                return Respuesta<bool>.Fault("El ID del cliente es obligatorio y debe ser mayor a cero.");

            if (factura.detalles == null || !factura.detalles.Any())
                return Respuesta<bool>.Fault("La factura debe contener al menos un producto.");

            return Respuesta<bool>.Success(true);
        }

        #endregion

        #region Validación General de Factura

        public Respuesta<bool> ValidarInserciónFactura(FacturaPeticionDto facturaPeticion, List<Producto> productos, List<Cliente> clientes)
        {
            var validarDatosFactura = ValidarDatosFactura(facturaPeticion);
            if (!validarDatosFactura.Ok)
                return validarDatosFactura;

            var validarCliente = VerificarExistenciaCliente(facturaPeticion.codigoCliente, clientes);
            if (!validarCliente.Ok)
                return validarCliente;

            foreach (var item in facturaPeticion.detalles)
            {
                var producto = productos.FirstOrDefault(p => p.ProductoId == item.ProductoId);
                if (producto == null)
                    return Respuesta<bool>.Fault($"El producto con ID {item.ProductoId} no existe.", "404");

                var validarCantidad = ValidarCantidadDisponibleProducto(producto, item.cantidaProducto);
                if (!validarCantidad.Ok)
                    return validarCantidad;
            }

            return Respuesta<bool>.Success(true);
        }

        #endregion
    }

}
