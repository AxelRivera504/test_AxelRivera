namespace test_AxelRivera.WebApi.Features.Reporte.Dto
{
    public class ReporteDto
    {
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public int? clienteId { get; set; }
        public int? productoId { get; set; }
    }
}
