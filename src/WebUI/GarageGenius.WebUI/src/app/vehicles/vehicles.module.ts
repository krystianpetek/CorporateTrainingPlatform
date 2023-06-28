import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { VehiclesRoutingModule } from './vehicles-routing.module';
import { VehicleListComponent } from './vehicle-list/vehicle-list.component';
import { VehicleAddComponent } from './vehicle-add/vehicle-add.component';
import { AppMaterialModule } from '../shared/app-material.module';
import { VehicleDetailsComponent } from './vehicle-details/vehicle-details.component';
import { ReactiveFormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';
import { VehicleReservationsComponent } from './vehicle-reservations/vehicle-reservations.component';

@NgModule({
  declarations: [
    VehicleListComponent,
    VehicleAddComponent,
    VehicleDetailsComponent,
    VehicleReservationsComponent,
  ],
  imports: [
    CommonModule,
    VehiclesRoutingModule,
    AppMaterialModule,
    ReactiveFormsModule,
    FlexLayoutModule,
  ],
})
export class VehiclesModule {}
