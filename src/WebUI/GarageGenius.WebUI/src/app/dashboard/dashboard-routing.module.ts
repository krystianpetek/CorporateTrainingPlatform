import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { authenticationGuard } from '../shared/guards/authentication.guard';
import { DashboardComponent } from './dashboard/dashboard.component';

const routes: Routes = [
  {
    path: '',
    component: DashboardComponent,
    canActivate: [authenticationGuard],
    children: [
      {
        path: 'users',
        pathMatch: 'prefix',
        loadChildren: () =>
          import('../users/users.module').then((m) => m.UsersModule),
        canActivate: [authenticationGuard],
      },
      {
        path: 'vehicles',
        pathMatch: 'prefix',
        loadChildren: () =>
          import('../vehicles/vehicles.module').then((m) => m.VehiclesModule),
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
