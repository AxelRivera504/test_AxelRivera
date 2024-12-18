export interface FacturaPeticionDto{
  codigoCliente:number,
  total:number,
  detalles: Array<FacturaPeticionDetalleDto>
}

export interface FacturaPeticionDetalleDto{
  productoId:number,
  cantidaProducto:number,
}

export interface FacturaProductoDataSource{
  productoId:number,
  productoNombre:string,
  cantidaProducto:number,
}

