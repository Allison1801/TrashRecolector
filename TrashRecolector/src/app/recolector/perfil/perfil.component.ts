import { Component, OnInit } from '@angular/core';
import { RecolectorService } from '../services/recolector.service';
import {  Usuario } from '../interfaces/usuario.interface';
import { Responsive } from '../interfaces/responsive.interface';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.css']
})
export class PerfilComponent implements OnInit{

  user!: Usuario ;
  id: number = 0;
  nombre: string="";
  apellido: string="";
  edad: number=0;
  correo: string="";
  contra:string="";
  
  
  constructor(private recolectorService: RecolectorService,private messageService : MessageService){}
 
  ngOnInit(): void {
    this.user=this.recolectorService.getLoginPerfil();
    this.obtenerUsuario();
  }




  obtenerUsuario(){
    this.recolectorService.getUsuarioPorId(this.user.id).subscribe( 
      (response:Responsive)=>{
        if (response.code === "00") {
          const usuario: Usuario = response.data;
          this.id = usuario.id;
          this.nombre = usuario.nombre;
          this.apellido = usuario.apellido;
          this.edad = usuario.edad;
          this.correo = usuario.correo;
          this.contra = usuario.contra;
        }else{
          console.error("Error en la solicitud: ", response.message);
        }
    
    },
    (error)=>{
      console.error("Error en la solicitud", error);
    })
  }

  modificarRegistro() {
    this.recolectorService.getUsuarioPorId(this.id).subscribe(
      (data) => {
        this.user = {
          id: this.id,
          nombre: this.nombre,
          apellido: this.apellido,
          edad: this.edad,
          correo: this.correo,
          contra: this.contra,
          estado:"inactivo"
        };
  
        this.recolectorService.putRegistrar(this.user).subscribe(
          (response: Responsive) => {
            if (response.code === "00") {
              console.log(this.user);
               this.MensajeActualizado(response);
            } else {
              console.error("Error en la solicitud: ", response.message);
            }
          },
          (error) => {
            console.error("Error en la solicitud: ", error);
          }
        );
      },
      (error) => {
        console.error("Error en la solicitud: ", error);
      }
    );
  }

     MensajeActualizado(response: Responsive) {
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

}
