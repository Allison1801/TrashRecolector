import { Component,  OnInit  } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {MenuItem, MessageService, PrimeNGConfig} from 'primeng/api';
import { RecolectorService } from 'src/app/recolector/services/recolector.service';
import { Usuario } from '../../recolector/interfaces/usuario.interface';
import { Comentario } from 'src/app/recolector/interfaces/comentario.interface';
import { PermissionGuard } from 'src/app/guards/permission.guard';
import { Responsive } from 'src/app/recolector/interfaces/responsive.interface';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
  
})
export class HeaderComponent implements OnInit {

  ngOnInitCalled: boolean = false;
  name : string = "";
  nuevoForm: FormGroup;
  visibleSidebar1: boolean = false;
  items: MenuItem[] =[];
  visible: boolean=false;

  _usuario : Usuario [] = [];
  user!:Usuario;
  id:number=0;
  nombre:string ="";
  apellido:string="";
  edad:number =0;
  correo:string="";
  contra:string="";

  comentario:string="";
  comentarios!:Comentario


   
  constructor(private primengConfig: PrimeNGConfig,private messageService: MessageService, private recolectorService:RecolectorService,private formBuilder: FormBuilder,private permision: PermissionGuard 
    )
    {
      this.nuevoForm = this.formBuilder.group({
        comentario: ['', Validators.required],
        correo:['', Validators.required]
      });
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
            this.contra = usuario.contra;
          } else {
            console.error("Error en la solicitud: ", response.message);
          }
        },
        (error) => {
          console.error("Error en la solicitud: ", error);
        }
      );
    }
  
    enviarComentario() {
      this.recolectorService.getUsuarioPorCorreo(this.correo).subscribe(
        (response: Responsive) => {
          if (response.code === "00") {
            const usuario: Usuario = response.data as Usuario;
    
            this.comentarios = {
              id: 0,
              comentario: this.comentario,
              correo: this.correo,
              usuario: usuario
            };
    
            this.recolectorService.PostComentarios(this.comentarios).subscribe(
              (response: Responsive) => {
                if (response.code === "00") {
                  this.visible = false;
                  this.MensajeComentario(response);
                } else {
                  console.error("Error en la solicitud: ", response.message);
                }
              },
              (error) => {
                console.error("Error en la solicitud: ", error);
              }
            );
          } else {
            console.error("Error en la solicitud: ", response.message);
          }
        },
        (error) => {
          console.error("Error en la solicitud: ", error);
        }
      );
    }
   
    MensajeComentario(response: Responsive) {
      if (response.code === "00") {
            this.messageService.add({
              severity: 'success',
              summary: 'Éxito',
              detail: response.message
            });
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

    





  

  ngOnInit(): void {
    this.user=this.recolectorService.getUsuarioComentario();
    if (this.ngOnInitCalled) {
      this.obtenerUsuario();
    }
    this.primengConfig.ripple = true
    this.items = [
     
      {
          label: 'Horarios',
          icon: 'fa fa-calendar',
          routerLink:'horarios'
          
      },
      {
        label: 'Mapa',
        icon: 'fa fa-map',
        routerLink:'mapa'
      },
      {
        label: 'Centros de acopio',
        icon: 'fa fa-recycle',
        routerLink:'centros'
      },
      {
        label: 'Consejos sustentable',
        icon: 'fa fa-envelope-open',
        routerLink:'consejos'
      },
      ]
      
    }
      
  
      showDialogInput() {
        if(this.permision.permiso===true){
          this.visible = true;
          this.ngOnInitCalled = true;
          this.ngOnInit();
        }else{
          alert('No puede acceder');
        }
      }

  
       
       showConfirm() {
          this.messageService.clear();
          this.messageService.add({key: 'c', sticky: true, severity:'warn', summary:'Estas seguro de salir?', detail:'Confirma el proceso'});
        }

      onReject() {
        this.messageService.clear('c');
    }
      onConfirm() {
        this.messageService.clear('c');
        this.recolectorService.CerrarSesion();
    }

     limpiarFormulario(){
      this.nuevoForm.reset();
     }

   
}
