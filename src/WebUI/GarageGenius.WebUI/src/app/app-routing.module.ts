import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HealthCheckComponent } from './health-check/health-check.component';

const routes: Routes = [
  {
    path: 'authentication',
    loadChildren: () =>
      import('./authentication/authentication-routing.module').then(
        (routing) => routing.AuthenticationRoutingModule
      ),
  },
  {
    path: 'dashboard',
    loadChildren: () =>
      import('./dashboard/dashboard-routing.module').then(
        (routing) => routing.DashboardRoutingModule
      ),
  },
  { path: 'health-check', component: HealthCheckComponent },
  { path: '**', redirectTo: '/dashboard' } // TODO - fallback
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
