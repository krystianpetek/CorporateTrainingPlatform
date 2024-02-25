import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {AccountComponent} from "./account/account.component";
import {RouterModule, Routes} from "@angular/router";
import {authenticationGuard} from "../shared/guards/authentication.guard";
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import {MatRadioModule} from "@angular/material/radio";

const routes: Routes = [
  {
    path: '',
    component: AccountComponent,
    canActivate: [authenticationGuard],
  }
]

@NgModule({
  declarations: [AccountComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MatButtonModule,
    MatIconModule,
    MatRadioModule
  ],
  exports: [RouterModule],
})
export class AccountModule { }
