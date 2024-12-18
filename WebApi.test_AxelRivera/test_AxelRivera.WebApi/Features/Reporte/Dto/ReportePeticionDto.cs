namespace test_AxelRivera.WebApi.Features.Reporte.Dto
{
    public class ReportePeticionDto
    {
        public int codigoCliente { get; set; }

        public string nombreCliente { get; set; } = null!;

        public string direccionCliente { get; set; } = null!;

        public string telefonoCliente { get; set; } = null!;

        public int idFactura { get; set; }

        public DateOnly fechaFactura { get; set; }

        public List<ReportePeticionDetalleDto> reportePeticionDetalleDto { get; set; }
    }
}
