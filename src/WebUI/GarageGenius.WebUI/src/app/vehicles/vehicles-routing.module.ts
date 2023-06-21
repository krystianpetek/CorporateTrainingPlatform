import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { VehiclesComponent } from './vehicle-list/vehicles.component';
import { authenticationGuard } from '../shared/guards/authentication.guard';
import { AddVehicleComponent } from './add-vehicle/add-vehicle.component';
import { VehicleComponent } from './vehicle/vehicle.component';

const routes: Routes = [
  {
    path: '',
    component: VehiclesComponent,
    pathMatch: 'full',
    canActivate: [authenticationGuard],
  },
  {
    path: 'new',
    component: AddVehicleComponent,
  },
  {
    path: ':id',
    component: VehicleComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class VehiclesRoutingModule {}
