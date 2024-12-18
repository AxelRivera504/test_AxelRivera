namespace test_AxelRivera.WebApi.Features.Facturas.Dto
{
    public class FacturaPeticionDto
    {
        public int codigoCliente { get; set; }
        public decimal total { get; set; }
        public List<FacturaPeticionDetalleDto> detalles { get; set; }
    }
}
