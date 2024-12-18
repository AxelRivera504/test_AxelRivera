namespace test_AxelRivera.WebApi.Features.Productos.Dto
{
    public class ProductoDtoPeticion
    {
        public int? productoId { get; set; }

        public string nombreProducto { get; set; } = null!;

        public string? descripcion { get; set; }

        public decimal precio { get; set; }

        public int cantidadDisponible { get; set; }

        public bool descontinuado { get; set; }
    }
}
