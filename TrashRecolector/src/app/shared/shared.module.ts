import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { PrimengModule } from '../primeng/primeng/primeng.module';
import { RecolectorModule } from '../recolector/recolector.module';
import { FormsModule } from '@angular/forms';








@NgModule({
  declarations: [
    HeaderComponent,
    FooterComponent,
    ],
  imports: [
    CommonModule,
    PrimengModule,
    RecolectorModule,

  ],
  exports:[
     HeaderComponent,
    FooterComponent,
   
   
  ]
})
export class SharedModule { }
