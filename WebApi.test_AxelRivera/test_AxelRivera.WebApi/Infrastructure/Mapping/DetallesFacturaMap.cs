using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using test_AxelRivera.WebApi.Infrastructure.Enitites;

namespace test_AxelRivera.WebApi.Infrastructure.Mapping
{
    public class DetallesFacturaMap : IEntityTypeConfiguration<DetallesFactura>
    {
        public void Configure(EntityTypeBuilder<DetallesFactura> builder)
        {
            builder.HasKey(e => e.DetalleId).HasName("PK_DetallesFactura_detalle_id");

            builder.ToTable("DetallesFactura");

            builder.Property(e => e.DetalleId).HasColumnName("detalle_id");
            builder.Property(e => e.Cantidad).HasColumnName("cantidad");
            builder.Property(e => e.FacturaId).HasColumnName("factura_id");
            builder.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio_unitario");
            builder.Property(e => e.ProductoId).HasColumnName("producto_id");

            builder.HasOne(d => d.Factura).WithMany(p => p.DetallesFacturas)
                .HasForeignKey(d => d.FacturaId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.Producto).WithMany(p => p.DetallesFacturas)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
