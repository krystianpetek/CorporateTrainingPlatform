import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReservationsRoutingModule } from './reservations-routing.module';
import { AppMaterialModule } from '../shared/app-material.module';
import { ReactiveFormsModule } from "@angular/forms";
import { ReservationAddComponent } from './reservation-add/reservation-add.component';
import { ReservationListComponent } from './reservation-list/reservation-list.component';
import { ReservationDetailsComponent } from './reservation-details/reservation-details.component';

@NgModule({
  declarations: [
    ReservationAddComponent,
    ReservationListComponent,
    ReservationDetailsComponent,
  ],
  imports: [CommonModule, ReservationsRoutingModule, AppMaterialModule, ReactiveFormsModule],
})
export class ReservationsModule {}
