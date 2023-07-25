import { Usuario } from "./usuario.interface";

export interface Comentario{
    id:number,
    comentario:string,
    correo:string,
    usuario: Usuario
}