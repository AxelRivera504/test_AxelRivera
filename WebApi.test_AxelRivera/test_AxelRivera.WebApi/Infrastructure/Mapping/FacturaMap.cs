using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using test_AxelRivera.WebApi.Infrastructure.Enitites;

namespace test_AxelRivera.WebApi.Infrastructure.Mapping
{
    public class FacturaMap : IEntityTypeConfiguration<Factura>
    {
        public void Configure(EntityTypeBuilder<Factura> builder)
        {
            builder.HasKey(e => e.FacturaId).HasName("PK_Facturas_factura_id");

            builder.Property(e => e.FacturaId).HasColumnName("factura_id");
            builder.Property(e => e.ClienteId).HasColumnName("cliente_id");
            builder.Property(e => e.Fecha).HasColumnName("fecha");
            builder.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
            .HasColumnName("total");

            builder.HasOne(d => d.Cliente).WithMany(p => p.Facturas).HasForeignKey(d => d.ClienteId);
        }
    }
}
