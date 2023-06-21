import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { VehiclesRoutingModule } from './vehicles-routing.module';
import { VehiclesComponent } from './vehicle-list/vehicles.component';
import { AddVehicleComponent } from './add-vehicle/add-vehicle.component';
import { AppMaterialModule } from '../shared/app-material.module';
import { VehicleComponent } from './vehicle/vehicle.component';

@NgModule({
  declarations: [VehiclesComponent, AddVehicleComponent, VehicleComponent],
  imports: [CommonModule, VehiclesRoutingModule, AppMaterialModule],
})
export class VehiclesModule {}
