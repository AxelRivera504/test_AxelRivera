import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs/internal/firstValueFrom';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Respuesta } from '../models/respuesta';
import { ProductoDto } from '../models/productodto';
import { environment } from '../../environment/environment.development';
import { ProductoPeticionDto } from '../models/producto-petitciondto';
import { ReporteDto } from '../models/reportedto';
import { ReportePeticionDto } from '../models/reporte-peticiondto';
import { ClienteDto } from '../models/clientedto';
import { FacturaPeticionDto } from '../models/facturas-peticionDto';

@Injectable({
  providedIn: 'root'
})

export class FacturacionService {

  constructor(private readonly _http: HttpClient) { }

  api:string= environment.webApis.Api;

  //#region Productos
  async ObtenerProductos(): Promise<Respuesta<Array<ProductoDto>>> {
    let endpoint = `${this.api}api/Productos`;
    return await firstValueFrom(this._http.get<Respuesta<Array<ProductoDto>>>(`${endpoint}/ObtenerProductos`));
  }

  async AgregarProducto(productoAgregar:ProductoPeticionDto): Promise<Respuesta<string>> {
    let endpoint = `${this.api}api/Productos`;
    const headers = new HttpHeaders({'Content-Type': 'application/json' });
    return await firstValueFrom(
      this._http.post<Respuesta<string>>(`${endpoint}/AgregarProductos`,
        JSON.stringify(productoAgregar),
        { headers }
      ))
  }

  async ActualizarProducto(productoActualizar:ProductoPeticionDto): Promise<Respuesta<string>> {
    let endpoint = `${this.api}api/Productos`;
    const headers = new HttpHeaders({'Content-Type': 'application/json' });
    return await firstValueFrom(
      this._http.put<Respuesta<string>>(`${endpoint}/ActualizarProductos`,
        JSON.stringify(productoActualizar),
        { headers }
      ))
  }

  async DescontinuarProducto(productoActualizar:ProductoPeticionDto): Promise<Respuesta<string>> {
    let endpoint = `${this.api}api/Productos`;
    const headers = new HttpHeaders({'Content-Type': 'application/json' });
    return await firstValueFrom(
      this._http.put<Respuesta<string>>(`${endpoint}/DesactivarProductos`,
        JSON.stringify(productoActualizar),
        { headers }
      ))
  }
  //#endregion

  //#region reporte
  async ObtenerReporte(reportedto:ReporteDto): Promise<Respuesta<Array<ReportePeticionDto>>> {
    let endpoint = `${this.api}api/Reporte`;
    const headers = new HttpHeaders({'Content-Type': 'application/json' });
    return await firstValueFrom(
      this._http.post<Respuesta<Array<ReportePeticionDto>>>(`${endpoint}/ObtenerReporte`,
        JSON.stringify(reportedto),
        { headers }
      ))
  }
  //#endregion

  //#region Clientes
  async ObtenerClientes(): Promise<Respuesta<Array<ClienteDto>>> {
    let endpoint = `${this.api}api/Clientes`;
    return await firstValueFrom(this._http.get<Respuesta<Array<ClienteDto>>>(`${endpoint}/ObtenerClientes`));
  }
  //#endregion

  //#region Facturar
  async AgregarFactura(facturaPeticionDto:FacturaPeticionDto): Promise<Respuesta<string>> {
    let endpoint = `${this.api}api/Facturas`;
    const headers = new HttpHeaders({'Content-Type': 'application/json' });
    return await firstValueFrom(
      this._http.post<Respuesta<string>>(`${endpoint}/AgregarFactura`,
        JSON.stringify(facturaPeticionDto),
        { headers }
      ))
  }
  //#endregion



}
