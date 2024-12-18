﻿using System;
using System.Collections.Generic;

namespace test_AxelRivera.WebApi.Infrastructure.Views;

public partial class VwReporteDetalladoVenta
{
    public int CodigoCliente { get; set; }

    public string NombreCliente { get; set; } = null!;

    public string DireccionCliente { get; set; } = null!;

    public string TelefonoCliente { get; set; } = null!;

    public int IdFactura { get; set; }

    public DateOnly FechaFactura { get; set; }

    public int ProductoId { get; set; }

    public string NombreProducto { get; set; } = null!;

    public int CantidadVendida { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal? TotalProducto { get; set; }
}