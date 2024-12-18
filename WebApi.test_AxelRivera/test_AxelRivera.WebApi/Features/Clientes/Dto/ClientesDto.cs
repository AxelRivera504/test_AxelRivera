namespace test_AxelRivera.WebApi.Features.Clientes.Dto
{
    public class ClientesDto
    {
        public int ClienteId { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Direccion { get; set; }

        public string? Telefono { get; set; }

        public string? Correo { get; set; }
    }
}
