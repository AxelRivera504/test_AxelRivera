import { Component } from '@angular/core';
import { FacturacionService } from '../../services/facturacion.service';
import { AlertsService } from '../../services/alerts.service';
import { FormBuilder, FormGroup, FormsModule, Validators } from '@angular/forms';
import { Respuesta } from '../../models/respuesta';
import { FacturaPeticionDetalleDto, FacturaPeticionDto, FacturaProductoDataSource } from '../../models/facturas-peticionDto';
import { DxButtonModule, DxDataGridModule, DxLookupModule, DxNumberBoxModule, DxPopupModule } from 'devextreme-angular';
import { ProductoDto } from '../../models/productodto';
import { ClienteDto } from '../../models/clientedto';

@Component({
  selector: 'app-facturacion',
  standalone: true,
  imports: [FormsModule, DxDataGridModule, DxButtonModule, DxLookupModule, DxPopupModule, DxNumberBoxModule], // Importa FormsModule aquí
  templateUrl: './facturacion.component.html',
  styleUrl: './facturacion.component.css'
})
export class FacturacionComponent {
  productosDataSource: Array<ProductoDto> = [];
  clientesDataSource: Array<ClienteDto> = [];
  productosFacturaDataSource: Array<FacturaProductoDataSource> = [];

  clienteSeleccionado: Partial<ClienteDto> = {};
  productoSeleccionado: ProductoDto = null;
  cantidadProducto: number = 1;

  subtotal: number = 0;
  isv: number = 0;
  total: number = 0;

  isEditPopupVisible: boolean = false;
  selectedProduct: FacturaProductoDataSource = null;
  editedCantidad: number = 0;

  constructor(
    private facturacionService: FacturacionService,
    private alerts: AlertsService
  ) {}

  ngOnInit(): void {
    this.cargarProductos();
    this.cargarClientes();
  }

  cargarProductos(): void {
    this.facturacionService.ObtenerProductos().then(res => {
      this.productosDataSource = res.data;
    });
  }

  cargarClientes(): void {
    this.facturacionService.ObtenerClientes().then(res => {
      this.clientesDataSource = res.data;
    });
  }

  onClienteSeleccionado(event: any): void {
    console.log(event.value);
    this.clienteSeleccionado = { clienteId: event.value };
  }

  onProductoSeleccionado(event: any): void {
    this.productoSeleccionado = this.productosDataSource.find(p => p.productoId === event.value);
  }

  agregarProducto(): void {
    if (!this.productoSeleccionado) {
      this.alerts.SwalError('Error', 'Debe seleccionar un producto');
      return;
    }

    if (this.cantidadProducto <= 0) {
      this.alerts.SwalError('Error', 'La cantidad debe ser mayor a 0');
      return;
    }

    if (this.cantidadProducto > this.productoSeleccionado.cantidadDisponible) {
      this.alerts.SwalError('Error', 'La cantidad excede el stock disponible');
      return;
    }

    const existe = this.productosFacturaDataSource.some(
      p => p.productoId === this.productoSeleccionado.productoId
    );

    if (!existe) {
      this.productosFacturaDataSource.push({
        productoId: this.productoSeleccionado.productoId,
        productoNombre: this.productoSeleccionado.nombreProducto,
        cantidaProducto: this.cantidadProducto,
      });

      this.productoSeleccionado = null;
      this.cantidadProducto = 1;
      this.recalcularTotales();
    } else {
      this.alerts.SwalError('Error', 'El producto ya está agregado');
    }
  }

  editarProducto(producto: FacturaProductoDataSource): void {
    this.selectedProduct = { ...producto };
    this.editedCantidad = producto.cantidaProducto;
    this.isEditPopupVisible = true;
  }

  saveProduct(): void {
    if (this.editedCantidad <= 0) {
      this.alerts.SwalError('Error', 'La cantidad debe ser mayor a 0.');
      return;
    }

    const productoOriginal = this.productosDataSource.find(p => p.productoId === this.selectedProduct.productoId);

    if (this.editedCantidad > productoOriginal.cantidadDisponible) {
      this.alerts.SwalError('Error', 'La cantidad excede el stock disponible.');
      return;
    }

    const index = this.productosFacturaDataSource.findIndex(p => p.productoId === this.selectedProduct.productoId);
    if (index > -1) {
      this.productosFacturaDataSource[index].cantidaProducto = this.editedCantidad;
      this.recalcularTotales();
    }

    this.isEditPopupVisible = false;
    this.selectedProduct = null;
  }

  cancelEdit(): void {
    this.isEditPopupVisible = false;
    this.selectedProduct = null;
  }

  eliminarProducto(producto: FacturaProductoDataSource): void {
    const index = this.productosFacturaDataSource.findIndex(p => p.productoId === producto.productoId);
    if (index > -1) {
      this.productosFacturaDataSource.splice(index, 1);
      this.recalcularTotales();
    }
  }

  recalcularTotales(): void {
    this.subtotal = this.productosFacturaDataSource.reduce(
      (sum, item) => sum + item.cantidaProducto * this.obtenerPrecioUnitario(item.productoId),
      0
    );
    this.isv = this.subtotal * 0.15;
    this.total = this.subtotal + this.isv;
  }

  obtenerPrecioUnitario(productoId: number): number {
    const producto = this.productosDataSource.find(p => p.productoId === productoId);
    return producto ? producto.precio : 0;
  }

  onRowRemoved(): void {
    this.recalcularTotales();
  }

  calculateSubtotal = (rowData: FacturaProductoDataSource): number => {
    return rowData.cantidaProducto * this.obtenerPrecioUnitario(rowData.productoId);
  };

  async guardarFactura(): Promise<void> {
    if (!this.productosFacturaDataSource.length) {
      this.alerts.SwalError('Error', 'Debe agregar productos a la factura');
      return;
    }

    const factura = {
      codigoCliente: this.clienteSeleccionado,
      reportePeticionDetalleDto: this.productosFacturaDataSource
    };

    const arrayProductos: Array<FacturaPeticionDetalleDto> = this.productosFacturaDataSource.map(element => {
      return {
        productoId: element.productoId,
        cantidaProducto: element.cantidaProducto
      };
    });

    const facturaPeticionDto: FacturaPeticionDto = {
      codigoCliente: this.clienteSeleccionado.clienteId,
      total: this.total,
      detalles: arrayProductos
    };

    this.alerts.SwalLoading();

    try {
      const respuesta = await this.facturacionService.AgregarFactura(facturaPeticionDto);
      if (respuesta.codigo === "200") {
        this.alerts.SwalClose();
        this.isEditPopupVisible = false;
        this.alerts.SwalSuccess("", respuesta.mensaje, 3500);

        this.reiniciarFactura();
      } else {
        this.alerts.SwalError("", respuesta.mensaje);
      }
    } catch (error) {
      console.log(error)
      this.handleError();
    }
  }

  handleError(): void {
    this.alerts.SwalError("Error", "Error al enviar la petición");
  }

  reiniciarFactura(): void {
    this.productosFacturaDataSource = [];

    this.clienteSeleccionado = null;
    this.productoSeleccionado = null;
    this.cantidadProducto = 1;

    this.subtotal = 0;
    this.isv = 0;
    this.total = 0;

    this.cargarProductos();
    this.cargarClientes();

    this.alerts.SwalSuccess('Éxito', 'Factura procesada y reiniciada correctamente');
  }

}
