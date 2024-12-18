export interface ReportePeticionDto{
  codigoCliente:number,
  nombreCliente:string,
  direccionCliente:string,
  telefonoCliente:string,
  idFactura:number,
  reportePeticionDetalleDto: Array<ReportePeticionDetalleDto>
}

export interface ReportePeticionDetalleDto{
  nombreProducto:number,
  cantidadVendida:number,
  precioUnitario:number,
}
