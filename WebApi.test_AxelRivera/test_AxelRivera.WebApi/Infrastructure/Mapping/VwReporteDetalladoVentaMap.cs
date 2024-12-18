using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using test_AxelRivera.WebApi.Infrastructure.Views;

namespace test_AxelRivera.WebApi.Infrastructure.Mapping
{
    public class VwReporteDetalladoVentaMap : IEntityTypeConfiguration<VwReporteDetalladoVenta>
    {
        public void Configure(EntityTypeBuilder<VwReporteDetalladoVenta> builder)
        {
            builder
               .HasNoKey()
               .ToView("Vw_ReporteDetalladoVentas");

            builder.Property(e => e.CantidadVendida).HasColumnName("cantidadVendida");
            builder.Property(e => e.CodigoCliente).HasColumnName("codigoCliente");
            builder.Property(e => e.DireccionCliente)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("direccionCliente");
            builder.Property(e => e.FechaFactura).HasColumnName("fechaFactura");
            builder.Property(e => e.IdFactura).HasColumnName("idFactura");
            builder.Property(e => e.ProductoId).HasColumnName("productoId");
            builder.Property(e => e.NombreCliente)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombreCliente");
            builder.Property(e => e.NombreProducto)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombreProducto");
            builder.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precioUnitario");
            builder.Property(e => e.TelefonoCliente)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefonoCliente");
            builder.Property(e => e.TotalProducto)
                .HasColumnType("decimal(21, 2)")
                .HasColumnName("totalProducto");
        }
    }
}
