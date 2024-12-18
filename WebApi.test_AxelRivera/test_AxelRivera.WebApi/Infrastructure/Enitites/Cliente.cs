namespace test_AxelRivera.WebApi.Infrastructure.Enitites;

public partial class Cliente
{
    public int ClienteId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();
}
