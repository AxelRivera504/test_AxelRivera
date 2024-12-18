using Microsoft.EntityFrameworkCore;
using test_AxelRivera.WebApi.Infrastructure.Enitites;
using test_AxelRivera.WebApi.Infrastructure.Mapping;
using test_AxelRivera.WebApi.Infrastructure.Views;

namespace test_AxelRivera.WebApi.Infrastructure;

public partial class TestAxelRiveraContext : DbContext
{
    public TestAxelRiveraContext()
    {
    }

    public TestAxelRiveraContext(DbContextOptions<TestAxelRiveraContext> options)
        : base(options)
    {
    }

    #region Tables
    public DbSet<Cliente> Clientes { get; set; }

    public DbSet<DetallesFactura> DetallesFacturas { get; set; }

    public DbSet<Factura> Facturas { get; set; }

    public DbSet<MovimientosInventario> MovimientosInventarios { get; set; }

    public DbSet<Producto> Productos { get; set; }
    #endregion

    #region Views
    public DbSet<VwReporteDetalladoVenta> VwReporteDetalladoVentas { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Maps

        #region Tables
        modelBuilder.ApplyConfiguration(new FacturaMap());
        modelBuilder.ApplyConfiguration(new ClienteMap());
        modelBuilder.ApplyConfiguration(new ProductoMap());
        modelBuilder.ApplyConfiguration(new DetallesFacturaMap());
        modelBuilder.ApplyConfiguration(new MovimientosInventarioMap());
        #endregion

        #region Views
        modelBuilder.ApplyConfiguration(new VwReporteDetalladoVentaMap());
        #endregion

        #endregion
        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
