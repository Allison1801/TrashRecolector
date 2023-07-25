import { Injectable } from '@angular/core';
import {  Usuario } from '../interfaces/usuario.interface';
import { Router } from '@angular/router';
import {MessageService } from 'primeng/api';
import { PermissionGuard } from 'src/app/guards/permission.guard';
import { Comentario} from '../interfaces/comentario.interface';
import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment.development';
import { Observable, Subject, catchError, of, tap } from 'rxjs';
import { Sector } from '../interfaces/sector.interface';
import { Responsive } from '../interfaces/responsive.interface';
import { ResponsiveList } from '../interfaces/ResponsiveList.interface';

@Injectable({
  providedIn: 'root'
})
export class RecolectorService {

 

  private _registro: Usuario [] =[]; 
  private _observacion: Comentario [] = [];
  private _usuario!: Usuario;

  name: string="";

 

 

  constructor( public router: Router, private permision: PermissionGuard, private httpClient : HttpClient) {
    //Recuparar la información almacenada en el localStorage
    this._registro =JSON.parse(localStorage.getItem('registro')!)||[];
    this._observacion =JSON.parse(localStorage.getItem('observacion')!)||[];
    
   }

    
    //Login
      getIngresar(correo: string, contrasena: string) {
        console.log(correo,contrasena)
        const params = new HttpParams()
          .set('correo', correo)
          .set('contrasena', contrasena);
        return this.httpClient.get<Responsive>(`${environment.apiUrl}/usuarios`,{params: params});
      }

      // obtenerUsuarioPorCorreoYContrasena(correo: string, contrasena: string): Observable<Responsive> {
      //   const params = new URLSearchParams({ correo, contrasena }).toString();
      //   const url = `${environment.apiUrl}?${params}`;
      //   return this.httpClient.get<Responsive>(url);
      // }

      getLoginPerfil(){
        return this._usuario
      }
      setLogin(_usuario:Usuario) : void{
        this._usuario = _usuario;
      }


     //Usuario
      getUsuarioPorId(id: number){
        const params = new HttpParams()
        .set('id',id)
        return this.httpClient.get<Responsive>(`${environment.apiUrl}/usuarios/id?`,{params:params})
      }

      PostRegistrar(user:Usuario){
        return this.httpClient.post<Responsive>(`${environment.apiUrl}/usuarios`,user);
      }

      putRegistrar(user: Usuario){
        console.log(user)
        return this.httpClient.put<Responsive>(`${environment.apiUrl}/usuarios/${user.id}`,user);
      }

      //Comentarios
      PostComentarios(comentarios:Comentario){
        console.log(comentarios);
         return this.httpClient.post<Responsive>(`${environment.apiUrl}/comentarios`,comentarios)
      }
  
      getUsuarioPorCorreo(correo: string){
        const params = new HttpParams()
        .set('correo',correo)
        return this.httpClient.get<Responsive>(`${environment.apiUrl}/usuarios/correo?`,{params:params})
      }

      getUsuarioPorCorreoContrasena(correo: string): Observable<Responsive>{
        //console.log(correo);
        const correoCodificado = encodeURIComponent(correo);
        return this.httpClient.get<Responsive>(`${environment.apiUrl}/usuarios/correo/${correoCodificado}`).pipe(
          tap((response: Responsive) => {
            console.log('Respuesta recibida del servidor:', response);
          })
        )
        
      }

      cambiarContrasena(id: number, nuevaContrasena: string): Observable<Responsive> {
        const usuarioActualizado = { Contra: nuevaContrasena };
        return this.httpClient.put<Responsive>(`${environment.apiUrl}/usuarios/${id}/cambiar-contrasena`, usuarioActualizado).pipe(
          tap((response: Responsive) => {
            console.log('Respuesta recibida del servidor:', response);
          })
        )
      }

      getUsuarioComentario(){
        return this._usuario
      }
      setUsuarioComentario(_usuario:Usuario) : void{
        this._usuario = _usuario;
      }

      //Sectores
      getSectores(){
        return this.httpClient.get<ResponsiveList<Sector>>(`${environment.apiUrl}/sector`)
      }
  
     CerrarSesion(){
      this.permision.permiso=false;
      this.router.navigateByUrl("/");
    }

    

 
}
