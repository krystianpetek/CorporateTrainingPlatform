import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { authenticationGuard } from '../shared/guards/authentication.guard';
import { DashboardComponent } from './dashboard/dashboard.component';

const usersModule = () =>
  import('../users/users.module').then((m) => m.UsersModule);
const vehiclesModule = () =>
  import('../vehicles/vehicles.module').then((m) => m.VehiclesModule);

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
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DashboardRoutingModule {}
