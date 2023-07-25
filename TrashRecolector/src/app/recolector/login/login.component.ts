import { Component, Input, OnInit } from '@angular/core';
import {MessageService } from 'primeng/api';
import { RecolectorService } from '../services/recolector.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PermissionGuard } from 'src/app/guards/permission.guard';
import { Router } from '@angular/router';
import { Usuario } from '../interfaces/usuario.interface';
import { Responsive } from '../interfaces/responsive.interface';
import { Login } from '../interfaces/login.interface';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],

  
})
export class LoginComponent {
  nuevoForm: FormGroup;
 
  usuario!: Usuario;
  private _usuario!: Usuario;
   private response!:Responsive;
   visible: boolean=false;
  correo : string ="";
  correoN : string ="";
  contra: string ="";
 

  
  constructor(
    private recolectorService: RecolectorService,
    private formBuilder: FormBuilder, 
    private permision: PermissionGuard,
    public router: Router,
    private messageService : MessageService)
    {
      this.nuevoForm = this.formBuilder.group({
        correo: ['', Validators.required],
        contra: ['', Validators.required],
        correoN:['', Validators.required]
      });
    }
 
   

  

    IngresarHome() {
      this.recolectorService.getIngresar(this.correo, this.contra).subscribe(
        (response: Responsive) => {
          console.log("HOLA")
          this._usuario = response.data as Usuario;
          this.recolectorService.setLogin(this._usuario);
          this.recolectorService.setUsuarioComentario(this._usuario);
          this.permision.permiso = true;
           this.MensajeHome(response);
        },
        (error) => {
          console.error("Error en la solicitud: ", error);
        }
      );
    }

 


    obtenerUsuario(){
      this.recolectorService.getUsuarioPorCorreoContrasena(this.correoN).subscribe( 
        (response:Responsive)=>{
          console.log(response);
          this.MensajeCambioContra(response);
            const usuario: Usuario = response.data;
      },
      (error)=>{
        console.error("Error en la solicitud", error);
      })
    }
   
    MensajeInvalid() {
      if (this.nuevoForm.controls['correo'].invalid || this.nuevoForm.controls['contra'].invalid) {
        this.messageService.add({
          severity: 'warn',
          summary: 'Advertencia',
          detail: 'Ingrese las credenciales'
        });
        this.limpiarFormulario();
      }
    }

     MensajeHome(response: Responsive) {
       if (response.code === "01") {
         this.messageService.add({
           severity: 'error',
           summary: 'Error',
           detail: response.message
         });
         this.limpiarFormulario();
         return;
       }

       if (response.code === "00") {
         this.router.navigate(['home']).then(() => {
           setTimeout(() => {
             this.messageService.add({
               severity: 'success',
               summary: 'Bienvenido',
               detail: ''
             });
           });
         });
       }
     }

     MensajeCambioContra(response: Responsive) {
      if (response.code === "01") {
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: response.message
        });
        this.limpiarFormulario();
        return;
      }

      if (response.code === "00") {
            this.messageService.add({
              severity: 'success',
              summary: 'Listo',
              detail: response.message
            });
       this.limpiarFormulario();
      
      }
    }

     
  
     showDialogInput() {
      this.visible = true;
  }

      limpiarFormulario()
      {
        this.nuevoForm.reset();
      }
 
}
