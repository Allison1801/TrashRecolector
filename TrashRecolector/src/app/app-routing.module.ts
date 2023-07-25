import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { RegistrarComponent } from './recolector/registrar/registrar.component';
import { HomeComponent } from './recolector/home/home.component';
import { LoginComponent } from './recolector/login/login.component';
import { PerfilComponent } from './recolector/perfil/perfil.component';
import { HorariosComponent } from './recolector/horarios/horarios.component';
import { MapaComponent } from './recolector/mapa/mapa.component';
import { CentrosAcopiosComponent } from './recolector/centros-acopios/centros-acopios.component';
import { ConsejosComponent } from './recolector/consejos/consejos.component';
import { PermissionGuard } from './guards/permission.guard';




const routes: Routes = [
  {
    path:'',
    component:LoginComponent,
    
  },
  {
    path:'registrar',
    component:RegistrarComponent,
    pathMatch:'full'
    
  },
  {
    path:'home',
    component:HomeComponent,
    pathMatch:'full',
    canActivate: [PermissionGuard]
  },
  {
    path:'perfil',
    component:PerfilComponent,
    pathMatch:'full',
    canActivate: [PermissionGuard]
  },
  {
    path:'horarios',
    component:HorariosComponent,
    pathMatch:'full',
    canActivate: [PermissionGuard]
  },
  {
    path:'mapa',
    component:MapaComponent,
    pathMatch:'full',
    canActivate: [PermissionGuard]
  },
  {
    path:'centros',
    component:CentrosAcopiosComponent,
    pathMatch:'full',
    canActivate: [PermissionGuard]
  },
  {
    path:'consejos',
    component:ConsejosComponent,
    pathMatch:'full',
    canActivate: [PermissionGuard]
  }
  

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})



export class AppRoutingModule { 

 
}
