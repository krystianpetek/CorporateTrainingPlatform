import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeRoutingModule } from './home-routing.module';
import { HomeComponent } from './home/home.component';
import { HeaderComponent } from './home/header/header.component';
import { AppMaterialModule } from '../shared/app-material.module';


@NgModule({
  declarations: [HomeComponent, HeaderComponent],
  imports: [
    CommonModule,
    HomeRoutingModule,
    AppMaterialModule,
  ]
})
export class HomeModule { }
