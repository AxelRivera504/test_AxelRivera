﻿namespace test_AxelRivera.WebApi.Infrastructure.Enitites;

public partial class DetallesFactura
{
    public int DetalleId { get; set; }

    public int FacturaId { get; set; }

    public int ProductoId { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public virtual Factura Factura { get; set; } = null!;

    public virtual Producto Producto { get; set; } = null!;
}
