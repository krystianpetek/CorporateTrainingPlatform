import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { VehiclesComponent } from './vehicles.component';
import { authenticationGuard } from '../shared/guards/authentication.guard';

const routes: Routes = [
  {
    path: '',
    component: VehiclesComponent,
    pathMatch: 'prefix',
    canActivate: [authenticationGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class VehiclesRoutingModule {}
