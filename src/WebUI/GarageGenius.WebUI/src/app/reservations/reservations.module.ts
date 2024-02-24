import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReservationsRoutingModule } from './reservations-routing.module';
import { AppMaterialModule } from '../shared/app-material.module';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { ReservationAddComponent } from './reservation-add/reservation-add.component';
import { ReservationListComponent } from './reservation-list/reservation-list.component';
import { ReservationDetailsComponent } from './reservation-details/reservation-details.component';
import {TableGgComponent} from "../shared/components/table-gg/table-gg.component";

@NgModule({
  declarations: [
    ReservationAddComponent,
    ReservationListComponent,
    ReservationDetailsComponent,
  ],
  imports: [CommonModule, ReservationsRoutingModule, AppMaterialModule, ReactiveFormsModule, TableGgComponent, FormsModule],
})
export class ReservationsModule {}
