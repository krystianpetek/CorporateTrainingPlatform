import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { VehiclesRoutingModule } from './vehicles-routing.module';
import { VehiclesComponent } from './vehicle-list/vehicles.component';
import { AddVehicleComponent } from './add-vehicle/add-vehicle.component';

@NgModule({
  declarations: [VehiclesComponent, AddVehicleComponent],
  imports: [CommonModule, VehiclesRoutingModule],
})
export class VehiclesModule {}
