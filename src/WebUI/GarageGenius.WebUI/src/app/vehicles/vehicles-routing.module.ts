import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { VehicleListComponent } from './vehicle-list/vehicle-list.component';
import { authenticationGuard } from '../shared/guards/authentication.guard';
import { VehicleAddComponent } from './vehicle-add/vehicle-add.component';
import { VehicleDetailsComponent } from './vehicle-details/vehicle-details.component';

const routes: Routes = [
  {
    path: '',
    component: VehicleListComponent,
    pathMatch: 'full',
    canActivate: [authenticationGuard],
  },
  {
    path: 'add',
    component: VehicleAddComponent,
    canActivate: [authenticationGuard],
  },
  {
    path: ':id',
    component: VehicleDetailsComponent,
    canActivate: [authenticationGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class VehiclesRoutingModule {}
