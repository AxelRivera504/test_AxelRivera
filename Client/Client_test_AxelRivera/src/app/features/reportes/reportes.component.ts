import { CommonModule, NgIf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { DxButtonModule, DxDataGridModule, DxDateBoxModule, DxLookupModule, DxPopupModule, DxTextBoxModule } from 'devextreme-angular';
import { FacturacionService } from '../../services/facturacion.service';
import { ActivatedRoute } from '@angular/router';
import { Respuesta } from '../../models/respuesta';
import { ProductoDto } from '../../models/productodto';
import { ProductoPeticionDto } from '../../models/producto-petitciondto';
import { AlertsService } from '../../services/alerts.service';
import { ReportePeticionDto } from '../../models/reporte-peticiondto';
import { ClienteDto } from '../../models/clientedto';
import { ReporteDto } from '../../models/reportedto';

@Component({
  selector: 'app-reportes',
  standalone: true,
  imports: [DxPopupModule, FormsModule,CommonModule,DxDataGridModule, DxButtonModule, DxTextBoxModule, DxDateBoxModule, DxLookupModule, ReactiveFormsModule],
  templateUrl: './reportes.component.html',
  styleUrl: './reportes.component.css'
})
export class ReportesComponent implements OnInit {
  minDate: Date = new Date(1950, 0, 1);
  reporteDataSource: Array<ReportePeticionDto> = [];
  productosDataSource: Array<any> = [];
  clientesDataSource: Array<any> = [];

  reporteForm: FormGroup;

  constructor(
    private facturacionService: FacturacionService,
    private alerts: AlertsService,
    private fb: FormBuilder
  ) {
    this.reporteForm = this.fb.group({
      fechaInicio: ['', Validators.required],
      fechaFin: ['', Validators.required],
      productoId: [''],
      clienteId: ['']
    });
  }

  ngOnInit(): void {
    this.onChargeProductos();
    this.onChargeClientes();
  }

  onChargeProductos(): void {
    this.facturacionService.ObtenerProductos()
      .then((respuesta: Respuesta<Array<any>>) => {
        this.productosDataSource = respuesta.data;
      })
      .catch(() => this.handleError('Error al cargar productos'));
  }

  onChargeClientes(): void {
    this.facturacionService.ObtenerClientes()
      .then((respuesta: Respuesta<Array<any>>) => {
        this.clientesDataSource = respuesta.data;
      })
      .catch(() => this.handleError('Error al cargar clientes'));
  }

  selectProducto(event: any): void {
    this.reporteForm.get('productoId')?.setValue(event.value);
  }

  selectCliente(event: any): void {
    this.reporteForm.get('clienteId')?.setValue(event.value);
  }

  calculateTotal(rowData: any): number {
    const cantidad = rowData.cantidadVendida || 0;
    const precio = rowData.precioUnitario || 0;
    const subtotal = cantidad * precio;
    const totalConISV = subtotal * 1.15;
    return parseFloat(totalConISV.toFixed(2));
  }


  async onSearchReporte(): Promise<void> {
    if (this.reporteForm.invalid) {
      this.alerts.SwalWarning('Debe llenar las fechas de inicio y fin.');
      return;
    }

    const reporteDto = {
      fechaInicio: this.reporteForm.value.fechaInicio,
      fechaFin: this.reporteForm.value.fechaFin,
      productoId: this.reporteForm.value.productoId || 0,
      clienteId: this.reporteForm.value.clienteId || 0
    };

    this.alerts.SwalLoading();
    try {
      const respuesta = await this.facturacionService.ObtenerReporte(reporteDto);
      this.reporteDataSource = respuesta.data;
      this.alerts.SwalClose();
    } catch {
      this.handleError('Error al obtener el reporte');
    }
  }

  handleError(mensaje: string): void {
    this.alerts.SwalError('Error', mensaje);
  }
}
