namespace test_AxelRivera.WebApi.Infrastructure.Enitites;

public partial class Producto
{
    public int ProductoId { get; set; }

    public string NombreProducto { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }

    public int CantidadDisponible { get; set; }

    public bool? Descontinuado { get; set; }

    public virtual ICollection<DetallesFactura> DetallesFacturas { get; set; } = new List<DetallesFactura>();

    public virtual ICollection<MovimientosInventario> MovimientosInventarios { get; set; } = new List<MovimientosInventario>();
}
