import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { authenticationGuard } from '../shared/guards/authentication.guard';
import { CommonModule } from '@angular/common';
import {UserListComponent} from "./user-list/user-list.component";
import {staffGuard} from "../shared/guards/staff.guard";

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: UserListComponent,
    canActivate: [staffGuard],
  },
];

@NgModule({
  imports: [CommonModule, RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class UsersRoutingModule {}
