import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RegistrarComponent } from './registrar/registrar.component';
import { HomeComponent } from './home/home.component';
import { PrimengModule } from '../primeng/primeng/primeng.module';
import { PerfilComponent } from './perfil/perfil.component';
import { HorariosComponent } from './horarios/horarios.component';
import { ConsejosComponent } from './consejos/consejos.component';
import { CentrosAcopiosComponent } from './centros-acopios/centros-acopios.component';
import { MapaComponent } from './mapa/mapa.component';
import { FormsModule } from '@angular/forms';








@NgModule({
  declarations: [
    LoginComponent,
    RegistrarComponent,
    HomeComponent,
    PerfilComponent,
    HorariosComponent,
    MapaComponent,
    ConsejosComponent,
    CentrosAcopiosComponent
  ],
  imports: [
    CommonModule,
    PrimengModule,
    FormsModule
  
  ],
  exports:[
    

  ], providers: [],
})
export class RecolectorModule { }
