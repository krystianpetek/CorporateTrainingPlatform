import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { VehiclesRoutingModule } from './vehicles-routing.module';
import { VehicleListComponent } from './vehicle-list/vehicle-list.component';
import { VehicleAddComponent } from './vehicle-add/vehicle-add.component';
import { AppMaterialModule } from '../shared/app-material.module';
import { VehicleDetailsComponent } from './vehicle-details/vehicle-details.component';

@NgModule({
  declarations: [
    VehicleListComponent,
    VehicleAddComponent,
    VehicleDetailsComponent,
  ],
  imports: [CommonModule, VehiclesRoutingModule, AppMaterialModule],
})
export class VehiclesModule {}
