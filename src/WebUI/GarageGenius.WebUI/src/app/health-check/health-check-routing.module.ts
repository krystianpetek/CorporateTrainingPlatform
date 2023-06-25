import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HealthCheckComponent } from './health-check/health-check.component';
import { adminGuard } from '../shared/guards/admin.guard';
import { Role } from '../shared/services/authentication/models/role.model';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: HealthCheckComponent,
    canActivate: [adminGuard],
    data: { roles: [Role.Administrator] },
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class HealthCheckRoutingModule {}
