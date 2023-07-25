import { Component, Input, OnInit } from '@angular/core';
import { RecolectorService } from '../services/recolector.service';
import { Usuario } from '../interfaces/usuario.interface';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Responsive } from '../interfaces/responsive.interface';

@Component({
  selector: 'app-registrar',
  templateUrl: './registrar.component.html',
  styleUrls: ['./registrar.component.css']
})
export class RegistrarComponent implements OnInit {
  visible: boolean=false;
  nuevoForm: FormGroup;
  id: number=0;
  nombre: string = "";
  apellido: string="";
  edad: number=0;
  correo: string="";
  contra:string="";
  contrasena:string="";
  Nuevacontra:string="";
  user!: Usuario ;
  ngOnInitCalled: boolean = false;
  

  constructor(private recolectorService: RecolectorService,public router: Router,private formBuilder: FormBuilder,private messageService : MessageService)
    {
      this.nuevoForm = this.formBuilder.group({
        nombre: ['', Validators.required],
        apellido: ['', Validators.required],
        correo: ['', Validators.required],
        contra: ['', Validators.required],
        contrasena: ['', Validators.required],
        Nuevacontra: ['', Validators.required],
      });
      
    }     
  ngOnInit(): void {
    if (this.ngOnInitCalled) {
       this.obtenerUsuario();
    }
  }
     
    AgregarRegistro() {
      this.user = {
        id: this.id,
        nombre: this.nombre,
        apellido: this.apellido,
        edad: this.edad,
        correo: this.correo,
        contra: "12345678",
        estado: "inactivo"
      };
    
      this.recolectorService.PostRegistrar(this.user).subscribe(
        (response: Responsive) => {
          console.log(this.user);
           this.MensajeRegistro(response);
        },
        (error) => {
          console.error("Error en la solicitud: ", error);
         
        }
      );
    }


    cambiarContrasena() {
      this.user = {
        id: this.id,
        nombre: this.nombre,
        apellido: this.apellido,
        edad: this.edad,
        correo: this.correo,
        contra: this.Nuevacontra,
        estado: "inactivo"
      };
      
        this.recolectorService.cambiarContrasena(this.user.id, this.user.contra)
          .subscribe(
            (response: Responsive) => {
              this.MensajeCambioContraseña(response);
            },
            (error) => {
              console.error('Error en la solicitud', error);
            }
          );
      
    }


    obtenerUsuario() {
      this.recolectorService.getUsuarioPorCorreo(this.user.correo).subscribe(
        (response: Responsive) => {
          if (response.code === "00") {
            const usuario: Usuario = response.data;
    
            this.id = usuario.id;
            this.nombre = usuario.nombre;
            this.apellido = usuario.apellido;
            this.edad = usuario.edad;
            this.correo = usuario.correo;
            this.contrasena = usuario.contra;
          } else {
            console.error("Error en la solicitud: ", response.message);
          }
        },
        (error) => {
          console.error("Error en la solicitud: ", error);
        }
      );
    }

    
    
  
       MensajeRegistro(response: Responsive) {
          if (response.code === "00") {
            this.showDialogInput();
            this.limpiarFormulario();
            //  this.router.navigate(['/']).then(() => {
              // setTimeout(() => {
                this.messageService.add({
                  severity: 'success',
                  summary: 'Éxito',
                  detail: response.message
                });
               
              // },5000);
            //  });
            
            
          }
         if (response.code === "01") {
          this.messageService.add({
            severity: 'error',
            summary: 'Error',
            detail: response.message 
          });
          
          return;
        }
       }

      MensajeCambioContraseña(response: Responsive) {
        if (response.code === "00") {
          this.showDialogInput();
          this.limpiarFormulario();
              this.messageService.add({
                severity: 'success',
                summary: 'Éxito',
                detail: response.message
              });
              this.visible = false;
         }
          if (response.code === "01") {
            this.messageService.add({
              severity: 'error',
              summary: 'Error',
              detail: response.message 
            });
            this.visible = true;
            return;
          }
     }

     limpiarFormulario()
     {
       this.nuevoForm.reset();
     }
     
      showDialogInput() {
          this.visible = true;
          this.ngOnInitCalled = true;
          this.ngOnInit();
        
      }

      onDialogHide() {
        console.log('El diálogo se ha cerrado');
        this.router.navigate(['/']);
      }

}
