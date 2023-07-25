import { Component, OnInit } from '@angular/core';
import { PrimeNGConfig } from 'primeng/api';
import { RecolectorService } from '../services/recolector.service';
import { Usuario } from '../interfaces/usuario.interface';

@Component({
  selector: 'app-horarios',
  templateUrl: './horarios.component.html',
  styleUrls: ['./horarios.component.css']
})
export class HorariosComponent implements OnInit {
  banner: Banner[] = [];

  user!:Usuario;
  id:number=0;
  nombre:string ="";
  apellido:string="";
  edad:number =0;
  correo:string="";
  contra:string="";
  constructor(private primengConfig: PrimeNGConfig, private recolectorService: RecolectorService){}

  ngOnInit() {
    this.banner = [
        {
           
            image:
                'assets/msj/msj2.jpg',
                  },
                  {       
             image:
                'assets/msj/msj5.jpg',
                },
                {   
              image:
                 'assets/msj/msj3.jpg',
                },
                {
                      
              image:
                'assets/msj/msj4.png',
              },
    ];
}


}
export interface Banner {
  image?: String;
}

