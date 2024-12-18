import { CommonModule, NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { DxButtonModule, DxDataGridModule, DxPopupModule, DxTextBoxModule } from 'devextreme-angular';
import { FacturacionService } from '../../services/facturacion.service';
import { ActivatedRoute } from '@angular/router';
import { Respuesta } from '../../models/respuesta';
import { ProductoDto } from '../../models/productodto';
import { ProductoPeticionDto } from '../../models/producto-petitciondto';
import { AlertsService } from '../../services/alerts.service';

@Component({
  selector: 'app-productos',
  standalone: true,
  imports: [DxPopupModule, FormsModule,CommonModule,DxDataGridModule, DxButtonModule, DxTextBoxModule],
  templateUrl: './productos.component.html',
  styleUrl: './productos.component.css'
})
export class ProductosComponent {
  productosDataSource: Array<ProductoDto> = [];

  isEditPopupVisible: boolean = false;
  isDeletePopupVisible: boolean = false;
  isCreating: boolean = false;
  selectedProduct: any = {};
  selectedProductDelete: any = {};


  constructor(private facturacionService:FacturacionService,private router: ActivatedRoute, private alerts:AlertsService) {
  }
  ngOnInit(): void {
    this.onChargeProductos();
  }

  onChargeProductos(){
    this.facturacionService.ObtenerProductos().then((respuesta: Respuesta<Array<ProductoDto>>) => {
      this.productosDataSource = respuesta.data;
    })
    .catch(error => {
      console.log(error);

    })
  }

  onValueChanged(event : any):void{
    console.log(event);
    const {name} = event;
  }
  enviarDatos(event: any):void {

  }

  openCreatePopup(): void {
    this.selectedProduct = { nombreProducto: '', descripcion: '', cantidadDisponible: null, precio: null };
    this.isCreating = true;
    this.isEditPopupVisible = true;
  }

  openEditPopup(product: any): void {
    this.selectedProduct = { ...product };
    this.isCreating = false;
    this.isEditPopupVisible = true;
  }

  async saveProduct(): Promise<void> {
    if (!this.validateProductFields()) {
      return;
    }

    const producto: ProductoPeticionDto = {
      cantidadDisponible: this.selectedProduct.cantidadDisponible,
      descripcion: this.selectedProduct.descripcion.trim(),
      nombreProducto: this.selectedProduct.nombreProducto.trim(),
      precio: this.selectedProduct.precio,
      productoId: this.isCreating ? null : this.selectedProduct.productoId,
      descontinuado: false
    };

    this.alerts.SwalLoading();

    if (this.isCreating) {
      await this.facturacionService.AgregarProducto(producto).then((respuesta: Respuesta<string>) => {
        this.handleResponse(respuesta, "Producto creado exitosamente");
      }).catch(() => this.handleError());
    } else {
      await this.facturacionService.ActualizarProducto(producto).then((respuesta: Respuesta<string>) => {
        this.handleResponse(respuesta, "Producto actualizado exitosamente");
      }).catch(() => this.handleError());
    }
  }

  validateProductFields(): boolean {
    if (!this.selectedProduct.nombreProducto || this.selectedProduct.nombreProducto.trim() === "") {
      this.alerts.SwalWarning("El nombre del producto no puede estar vacío");
      return false;
    }

    if (!this.selectedProduct.descripcion || this.selectedProduct.descripcion.trim() === "") {
      this.alerts.SwalWarning("La descripción no puede estar vacía");
      return false;
    }

    const cantidad = Number(this.selectedProduct.cantidadDisponible);
    if (isNaN(cantidad) || cantidad <= 0) {
      this.alerts.SwalWarning("La cantidad disponible debe ser un número mayor a cero");
      return false;
    }

    const precio = Number(this.selectedProduct.precio);
    if (isNaN(precio) || precio <= 0) {
      this.alerts.SwalWarning("El precio debe ser un número mayor a cero");
      return false;
    }

    return true;
  }

  handleResponse(respuesta: Respuesta<string>, successMessage: string): void {
    if (respuesta.codigo === "200") {
      this.alerts.SwalClose();
      this.isEditPopupVisible = false;
      this.alerts.SwalSuccess("", successMessage, 3500);
      this.onChargeProductos();
    } else {
      this.alerts.SwalError("", respuesta.mensaje);
    }
  }

  handleError(): void {
    this.alerts.SwalError("Error", "Error al enviar la petición");
  }

  cancelEdit(): void {
    this.isEditPopupVisible = false;
  }

  async openDeletePopup(product: any): Promise<void> {
    this.selectedProduct = { ...product };
    this.alerts.SwalQuestion('Confirmación de Datos', '¿Está seguro que desea descontinuar este producto?', async () => {
      const producto: ProductoPeticionDto = {
        cantidadDisponible: this.selectedProduct.cantidadDisponible,
        descripcion: this.selectedProduct.descripcion.trim(),
        nombreProducto: this.selectedProduct.nombreProducto.trim(),
        precio: this.selectedProduct.precio,
        productoId: this.selectedProduct.productoId,
        descontinuado: true
      };

      this.alerts.SwalLoading();

      try {
        const respuesta = await this.facturacionService.DescontinuarProducto(producto);
        if (respuesta.codigo === "200") {
          this.alerts.SwalClose();
          this.isEditPopupVisible = false;
          this.alerts.SwalSuccess("", respuesta.mensaje, 3500);
          this.onChargeProductos();
        } else {
          this.alerts.SwalError("", respuesta.mensaje);
        }
      } catch (error) {
        this.handleError();
      }
    });
  }

}
