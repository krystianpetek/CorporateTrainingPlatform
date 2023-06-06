import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {SignInComponent} from "./sign-in/sign-in.component";
import {SignUpComponent} from "./sign-up/sign-up.component";
import { authenticationGuard } from './authentication.guard';

const routes: Routes = [
  {
    path: 'sign-up', component: SignUpComponent, canActivate: [authenticationGuard]
    // TODO - remove guard in future
  },
  {path: 'sign-in', component: SignInComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthenticationRoutingModule {
}
