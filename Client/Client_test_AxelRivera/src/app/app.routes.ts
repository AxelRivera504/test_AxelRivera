import { Routes } from '@angular/router';
import { ProductosComponent } from './features/productos/productos.component';
import { ReportesComponent } from './features/reportes/reportes.component';
import { FacturacionComponent } from './features/facturacion/facturacion.component';

export const routes: Routes = [
  { path: 'productos', component: ProductosComponent },
  { path: 'reporte', component: ReportesComponent },
  { path: 'facturar', component: FacturacionComponent },
  { path: '', redirectTo: 'productos', pathMatch: 'full' }, // Ruta raíz redirecciona a "productos"
  { path: '**', redirectTo: 'productos', pathMatch: 'full' } // Ruta wildcard para manejar rutas no válidas
];

