import { Injectable } from '@angular/core';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class AlertsService {

  constructor() { }

  SwalSuccess(titulo: string, mensaje = '', duration: number = 2000){
    Swal.fire(
      {
        icon: 'success',
        title: titulo,
        text: mensaje,
        timer: duration,
        allowEnterKey: false,
        allowEscapeKey: false,
        allowOutsideClick: false,
        showConfirmButton: false
      });
  }


  SwalError(titulo: string, mensaje: string){
    Swal.fire(
      {
        icon: 'error',
        title: titulo,
        text: mensaje,
        showConfirmButton: true,
        confirmButtonText: 'Ok',
        customClass: {
          confirmButton: 'custom-error-button'
        },
        allowEnterKey: false,
        allowEscapeKey: false,
        allowOutsideClick: false,
      });
  }

  SwalQuestion(titulo: string, mensaje = '', functionToDo:any){
    Swal.fire(
      {
        icon: 'question',
        title: titulo,
        html:mensaje,
        allowEnterKey: false,
        allowEscapeKey: false,
        allowOutsideClick: false,
        showConfirmButton: true,
        showDenyButton: true,
        confirmButtonText: 'Sí, estoy seguro,',
        denyButtonText:"No, escoger otra",
        customClass: {
          confirmButton: 'custom-confirm-button'
        },
        showClass:{
          popup:'animated fadeInDown'
        },
        hideClass:{
          popup:'animated fadeOutUp'
        }
      }).then( result => {
        if (result.isConfirmed) {
          this.SwalLoading('Procesando Solicitud');
          functionToDo();
        }
      });
  }

  SwalWarning(titulo: string = '', mensaje: string = '', duration: number = 1500){
    Swal.fire(
      {
        icon: 'warning',
        title: titulo,
        text: mensaje,
        customClass: {
          confirmButton: 'custom-warning-button'
        },
        // timer: duration,
        allowEnterKey: false,
        allowEscapeKey: false,
        allowOutsideClick: false,
        showConfirmButton: true
      });
  }

  SwalLoading(titulo: string = 'Procesando solicitud', mensaje: string = 'Espere un momento por favor...'){
    Swal.fire({
      title: titulo,
      text: mensaje,
      allowEnterKey: false,
      allowEscapeKey: false,
      allowOutsideClick: false,
      showConfirmButton: false,
      willOpen: () => {
        Swal.showLoading(null);
      }
    });
  }

  SwalClose(){
    Swal.close();
  }


}
