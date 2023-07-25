import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PermissionGuard implements CanActivate {
  permiso: boolean=false;

  constructor( public router: Router){
   
  }

  canActivate(): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    if(this.login()){
     return true;  
    }
    alert('No puede acceder')
    return false;
  }

  login():boolean{
    return this.permiso;
  }
  
}
