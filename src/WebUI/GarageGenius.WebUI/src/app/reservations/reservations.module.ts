import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReservationsRoutingModule } from './reservations-routing.module';
import { ReservationAddComponent } from './reservation-add/reservation-add.component';
import { ReservationListComponent } from './reservation-list/reservation-list.component';
import { ReservationDetailsComponent } from './reservation-details/reservation-details.component';
import { AppMaterialModule } from '../shared/app-material.module';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";

@NgModule({
  declarations: [
    ReservationAddComponent,
    ReservationListComponent,
    ReservationDetailsComponent,
  ],
    imports: [CommonModule, ReservationsRoutingModule, AppMaterialModule, FormsModule, ReactiveFormsModule],
})
export class ReservationsModule {}
