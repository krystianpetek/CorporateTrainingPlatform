import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { VehiclesRoutingModule } from './vehicles-routing.module';
import { VehiclesComponent } from './vehicle-list/vehicles.component';
import { AddVehicleComponent } from './add-vehicle/add-vehicle.component';
import { AppMaterialModule } from '../shared/app-material.module';
import { VehicleDetailsComponent } from './vehicle-details/vehicle-details.componentv';

@NgModule({
  declarations: [
    VehiclesComponent,
    AddVehicleComponent,
    VehicleDetailsComponent,
  ],
  imports: [CommonModule, VehiclesRoutingModule, AppMaterialModule],
})
export class VehiclesModule {}
