namespace test_AxelRivera.WebApi.Features.Productos.Dto
{
    public class ProductoDto
    {
        public int ProductoId { get; set; }

        public string NombreProducto { get; set; } = null!;

        public string? Descripcion { get; set; }

        public decimal Precio { get; set; }

        public int CantidadDisponible { get; set; }

        public bool Descontinuado { get; set; }
    }
}
