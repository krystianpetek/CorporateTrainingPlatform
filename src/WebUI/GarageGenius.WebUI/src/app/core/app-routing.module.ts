import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ErrorComponent } from '../shared/components/error/error.component';
import { authenticationGuard } from '../shared/guards/authentication.guard';
import { HomeComponent } from '../home/home/home.component';

const dashboardModule = () =>
  import('../dashboard/dashboard.module').then(
    (routing) => routing.DashboardModule
  );
const authenticationModule = () =>
  import('../authentication/authentication.module').then(
    (routing) => routing.AuthenticationModule
  );
const healthCheckModule = () =>
  import('../health-check/health-check.module').then(
    (routing) => routing.HealthCheckModule
  );

const routes: Routes = [
  {
    path: 'home',
    component: HomeComponent,
  },
  {
    path: 'authentication',
    loadChildren: authenticationModule,
  },
  {
    path: 'dashboard',
    loadChildren: dashboardModule,
    canActivate: [authenticationGuard],
  },
  {
    path: 'check-health',
    loadChildren: healthCheckModule,
    canActivate: [authenticationGuard],
  },
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: '**', component: ErrorComponent },
  // { path: '**', redirectTo: '404' }, // hidden information about before routes after redirection
  // { path: '404', component: ErrorComponent },
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
