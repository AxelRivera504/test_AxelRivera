namespace test_AxelRivera.WebApi.Features.Reporte.Dto
{
    public class ReportePeticionDetalleDto
    {
        public string NombreProducto { get; set; } = null!;

        public int CantidadVendida { get; set; }

        public decimal PrecioUnitario { get; set; }

    }
}
