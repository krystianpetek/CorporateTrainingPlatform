import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { authenticationGuard } from '../shared/guards/authentication.guard';
import { DashboardComponent } from './dashboard/dashboard.component';
import {
  PendingReservationsComponent
} from "../pending-reservations/pending-reservations/pending-reservations.component";

const usersModule = () =>
  import('../users/users.module').then((m) => m.UsersModule);
const vehiclesModule = () =>
  import('../vehicles/vehicles.module').then((m) => m.VehiclesModule);
const customersModule = () =>
  import('../customers/customers.module').then((m) => m.CustomersModule);
const reservationsModule = () =>
  import('../reservations/reservations.module').then(
    (m) => m.ReservationsModule
  );
const routes: Routes = [
  {
    path: '',
    component: DashboardComponent,
    canActivate: [authenticationGuard],
    children: [
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'vehicles',
      },
      {
        path: 'users',
        pathMatch: 'prefix',
        loadChildren: usersModule,
        canActivate: [authenticationGuard],
      },
      {
        path: 'vehicles',
        pathMatch: 'prefix',
        loadChildren: vehiclesModule,
        canActivate: [authenticationGuard],
      },
      {
        path: 'customers',
        loadChildren: customersModule,
        canActivate: [authenticationGuard],
      },
      {
        path: 'reservations',
        loadChildren: reservationsModule,
        canActivate: [authenticationGuard],
      },
      {
        path: 'pending-reservations',
        component: PendingReservationsComponent,
        canActivate: [authenticationGuard],
      }
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DashboardRoutingModule {}
