import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { authenticationGuard } from '../shared/guards/authentication.guard';
import { ReservationListComponent } from './reservation-list/reservation-list.component';

const routes: Routes = [
  {
    path: '',
    component: ReservationListComponent,
    canActivate: [authenticationGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ReservationsRoutingModule {}
