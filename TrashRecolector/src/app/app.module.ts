import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SharedModule } from './shared/shared.module';
import { RecolectorModule } from './recolector/recolector.module';
import { HttpClientModule } from '@angular/common/http';
import { DialogService } from 'primeng/dynamicdialog';
import { MessageService } from 'primeng/api';





@NgModule({
  declarations: [
    AppComponent,
 

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    SharedModule,
    RecolectorModule,
    HttpClientModule
  ], 

  providers: [DialogService,MessageService],
  bootstrap: [AppComponent]
})
export class AppModule { }
