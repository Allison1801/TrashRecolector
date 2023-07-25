import { NgModule } from '@angular/core';
import {PanelMenuModule} from 'primeng/panelmenu';
import {SidebarModule} from 'primeng/sidebar';
import {MenuModule} from 'primeng/menu';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ButtonModule } from 'primeng/button';
import {CarouselModule} from 'primeng/carousel';
import { ImageModule } from 'primeng/image';
import {DialogModule} from 'primeng/dialog';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {InputTextModule } from 'primeng/inputtext';
import {ToastModule} from 'primeng/toast';
import { DynamicDialogModule } from 'primeng/dynamicdialog';




@NgModule({
  declarations: [],
  
  exports:[
    PanelMenuModule,
    MenuModule,
    SidebarModule,
    BrowserModule,
    BrowserAnimationsModule,
    ButtonModule,
    CarouselModule,
    ImageModule,
    DialogModule,
    FormsModule,
    ReactiveFormsModule,
    InputTextModule,
    ToastModule,
    DynamicDialogModule
  ]
})
export class PrimengModule { }
