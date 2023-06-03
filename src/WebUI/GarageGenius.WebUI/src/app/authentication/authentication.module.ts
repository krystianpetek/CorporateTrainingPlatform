import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SignInComponent} from './sign-in/sign-in.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { AuthenticationRoutingModule } from './authentication-routing.module';

@NgModule({
  declarations: [
    SignUpComponent,
    SignInComponent,
  ],
  exports: [
    SignUpComponent,
    SignInComponent,
  ],
  imports: [
    CommonModule,
    AuthenticationRoutingModule,
  ]
})
export class AuthenticationModule { }
