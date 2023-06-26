import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomerListComponent } from './customer-list/customer-list.component';
import { authenticationGuard } from '../shared/guards/authentication.guard';

const routes: Routes = [
  {
    path: '',
    component: CustomerListComponent,
    canActivate: [authenticationGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CustomersRoutingModule {}
