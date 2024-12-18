using System;
using System.Collections.Generic;

namespace test_AxelRivera.WebApi.Infrastructure.Enitites;

public partial class Factura
{
    public int FacturaId { get; set; }

    public int? ClienteId { get; set; }

    public DateOnly Fecha { get; set; }

    public decimal Total { get; set; }

    public virtual Cliente? Cliente { get; set; }

    public virtual ICollection<DetallesFactura> DetallesFacturas { get; set; } = new List<DetallesFactura>();
}
