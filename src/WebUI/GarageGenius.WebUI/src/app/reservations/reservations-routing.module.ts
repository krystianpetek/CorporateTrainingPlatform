import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { authenticationGuard } from '../shared/guards/authentication.guard';
import { ReservationListComponent } from './reservation-list/reservation-list.component';
import { ReservationDetailsComponent } from './reservation-details/reservation-details.component';
import { ReservationAddComponent } from './reservation-add/reservation-add.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: ReservationListComponent,
    canActivate: [authenticationGuard],
  },
  {
    path: ':id',
    component: ReservationDetailsComponent,
    canActivate: [authenticationGuard],
  },
  {
    path: 'add',
    component: ReservationAddComponent,
    canActivate: [authenticationGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ReservationsRoutingModule {}
