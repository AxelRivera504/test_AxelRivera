using System;
using System.Collections.Generic;

namespace test_AxelRivera.WebApi.Infrastructure.Enitites;

public partial class MovimientosInventario
{
    public int MovimientoId { get; set; }

    public int ProductoId { get; set; }

    public string TipoMovimiento { get; set; } = null!;

    public int Cantidad { get; set; }

    public DateOnly Fecha { get; set; }

    public virtual Producto Producto { get; set; } = null!;
}
