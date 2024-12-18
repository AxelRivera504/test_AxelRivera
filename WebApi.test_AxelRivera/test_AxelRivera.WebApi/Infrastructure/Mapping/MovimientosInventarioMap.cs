using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using test_AxelRivera.WebApi.Infrastructure.Enitites;
using static Dapper.SqlMapper;

namespace test_AxelRivera.WebApi.Infrastructure.Mapping
{
    public class MovimientosInventarioMap : IEntityTypeConfiguration<MovimientosInventario>
    {
        public void Configure(EntityTypeBuilder<MovimientosInventario> builder)
        {
            builder.HasKey(e => e.MovimientoId).HasName("PK_MovimientosInventario_movimiento_id");

            builder.ToTable("MovimientosInventario");

            builder.Property(e => e.MovimientoId).HasColumnName("movimiento_id");
            builder.Property(e => e.Cantidad).HasColumnName("cantidad");
            builder.Property(e => e.Fecha).HasColumnName("fecha");
            builder.Property(e => e.ProductoId).HasColumnName("producto_id");
            builder.Property(e => e.TipoMovimiento)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("tipo_movimiento");

            builder.HasOne(d => d.Producto).WithMany(p => p.MovimientosInventarios)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
