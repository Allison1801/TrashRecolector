import { Component, OnInit } from '@angular/core';
import { RecolectorService } from '../services/recolector.service';
import { Sector } from '../interfaces/sector.interface';
import { ResponsiveList } from '../interfaces/ResponsiveList.interface';

@Component({
  selector: 'app-mapa',
  templateUrl: './mapa.component.html',
  styleUrls: ['./mapa.component.css']
})
export class MapaComponent implements OnInit {
  sector!: Sector
  _sectores: Sector[]=[]; // Declara una variable para almacenar los datos de la tabla


  constructor(private recolectorService: RecolectorService){}
  
  ngOnInit(): void {
    this.obtenerSectores();
  }


  
  obtenerSectores() {
    this.recolectorService.getSectores().subscribe(
      (response: ResponsiveList<Sector[]>) => {
        console.log("Response:", response);
  
        if (response && response.data) {
          if (Array.isArray(response.data)) {
            this._sectores = response.data;
            console.log("Sectores:", this._sectores);
          } else {
            console.error("Error en la estructura de datos: response.data no es un array");
          }
        } else {
          console.error("Error en la estructura de datos: respuesta o response.data es nula o indefinida");
        }
      },
      (error) => {
        console.error("Error en la petición: ", error);
      }
    );
  }
  


}
