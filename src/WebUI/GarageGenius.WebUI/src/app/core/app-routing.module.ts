import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HealthCheckComponent } from '../health-check/health-check.component';
import { ErrorComponent } from '../shared/components/error/error.component';
import { authenticationGuard } from '../shared/guards/authentication.guard';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  {
    path: 'home',
    loadChildren: () =>
      import('../home/home.module').then((routing) => routing.HomeModule),
  },
  {
    path: 'dashboard',
    loadChildren: () =>
      import('../dashboard/dashboard.module').then(
        (routing) => routing.DashboardModule
      ),
    canActivate: [authenticationGuard],
  },
  {
    path: 'authentication',
    loadChildren: () =>
      import('../authentication/authentication.module').then(
        (routing) => routing.AuthenticationModule
      ),
  },
  { path: 'health-check', component: HealthCheckComponent },
  { path: '**', component: ErrorComponent }, // fallback
  //{ path: '**', redirectTo: 'error', }
];

@NgModule({
  imports: [
    RouterModule.forRoot(
      routes
      // { enableTracing: true }
    ),
  ],
  exports: [RouterModule],
})
export class AppRoutingModule {}
