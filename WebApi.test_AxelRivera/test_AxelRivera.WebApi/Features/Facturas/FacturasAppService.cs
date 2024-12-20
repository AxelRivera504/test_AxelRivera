﻿using Microsoft.EntityFrameworkCore;
using test_AxelRivera.WebApi.Common;
using test_AxelRivera.WebApi.Features.Facturas.Dto;
using test_AxelRivera.WebApi.Infrastructure;
using test_AxelRivera.WebApi.Infrastructure.Enitites;

namespace test_AxelRivera.WebApi.Features.Facturas
{
    public class FacturasAppService : IFacturasAppService
    {
        private readonly TestAxelRiveraContext _testAxelRiveraContext;
        private readonly FacturaDomainService _facturaDomainService;
        public FacturasAppService(TestAxelRiveraContext testAxelRiveraContext, FacturaDomainService facturaDomainService)
        {
            _testAxelRiveraContext = testAxelRiveraContext;
            _facturaDomainService = facturaDomainService;
        }
        public async Task<Respuesta<string>> AgregarFactura(FacturaPeticionDto facturaPeticionDtos)
        {
            try
            {
                List<Producto> productos = _testAxelRiveraContext.Productos.AsQueryable().ToList();
                List<Cliente> clientes = _testAxelRiveraContext.Clientes.AsQueryable().ToList();

                Respuesta<bool> repuesta = _facturaDomainService.ValidarInserciónFactura(facturaPeticionDtos, productos, clientes);
                if (!repuesta.Ok)
                    return Respuesta<string>.Fault(repuesta.Mensaje, CodigosHttp.SERVER_ERROR_CODE);

                Factura factura = new Factura()
                {
                    ClienteId = facturaPeticionDtos.codigoCliente,
                    Fecha = DateOnly.FromDateTime(DateTime.Now),
                    Total = facturaPeticionDtos.total
                };

                await _testAxelRiveraContext.Facturas.AddAsync(factura);
                await _testAxelRiveraContext.SaveChangesAsync();

                List<DetallesFactura> detalles = new List<DetallesFactura>();
                List<MovimientosInventario> movimiento = new List<MovimientosInventario>();
                foreach (var item in facturaPeticionDtos.detalles)
                {
                    DetallesFactura detallesFactura = new DetallesFactura()
                    {
                        Cantidad = item.cantidaProducto,
                        FacturaId = factura.FacturaId,
                        ProductoId = item.ProductoId,
                        PrecioUnitario = productos.Find(x => x.ProductoId == item.ProductoId)!.Precio
                    };

                    MovimientosInventario movimientosInventario = new MovimientosInventario()
                    {
                        Cantidad = item.cantidaProducto,
                        Fecha = DateOnly.FromDateTime(DateTime.Now),
                        ProductoId = item.ProductoId,
                        TipoMovimiento = TipoMovimientoProductos.SALIDA
                    };

                    Producto productoBuscado = await _testAxelRiveraContext.Productos.FirstAsync(x => x.ProductoId == item.ProductoId);
                    productoBuscado.CantidadDisponible -= item.cantidaProducto;
                    await _testAxelRiveraContext.SaveChangesAsync();


                    movimiento.Add(movimientosInventario);
                    detalles.Add(detallesFactura);
                }

                await _testAxelRiveraContext.DetallesFacturas.AddRangeAsync(detalles);
                await _testAxelRiveraContext.MovimientosInventarios.AddRangeAsync(movimiento);
                await _testAxelRiveraContext.SaveChangesAsync();

                List<MovimientosInventario> movimientosInventarios = new List<MovimientosInventario>();

                return Respuesta<string>.Success("", Mensajes.FACTURA_CREADA_EXITOSAMENTE, CodigosHttp.SUCCESS_CODE);
            }
            catch (Exception ex)
            {
                return Respuesta<string>.Fault(string.Format(Mensajes.PROCESO_ERROR, ex.Message), CodigosHttp.SERVER_ERROR_CODE);
            }
        }
    }
}
