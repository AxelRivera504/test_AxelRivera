using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using test_AxelRivera.WebApi.Infrastructure.Enitites;

namespace test_AxelRivera.WebApi.Infrastructure.Mapping
{
    public class ProductoMap : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.HasKey(e => e.ProductoId).HasName("PK_Productos_producto_id");

            builder.Property(e => e.ProductoId).HasColumnName("producto_id");
            builder.Property(e => e.CantidadDisponible).HasColumnName("cantidad_disponible");
            builder.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            builder.Property(e => e.NombreProducto)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre_producto");
            builder.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");
            builder.Property(e => e.Descontinuado).HasColumnName("descontinuado");
        }
    }
}
