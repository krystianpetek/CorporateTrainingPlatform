import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HealthCheckComponent } from './health-check/health-check.component';
import { ErrorComponent } from './shared/components/error/error.component';
import { authenticationGuard } from './shared/guards/authentication.guard';
import { HomeComponent } from './home/home/home.component';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent, },
  {
    path: 'dashboard',
    loadChildren: () =>
      import('./dashboard/dashboard-routing.module').then(
        (routing) => routing.DashboardRoutingModule
      ),
    canActivate: [authenticationGuard]
  },
  {
    path: 'authentication',
    loadChildren: () =>
      import('./authentication/authentication-routing.module').then(
        (routing) => routing.AuthenticationRoutingModule
      ),
  },
  { path: 'health-check', component: HealthCheckComponent },
  { path: '**', component: ErrorComponent }, // fallback
  //{ path: '**', redirectTo: 'error', }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
