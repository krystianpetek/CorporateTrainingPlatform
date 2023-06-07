import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { SignInComponent} from './sign-in/sign-in.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { AuthenticationRoutingModule } from './authentication-routing.module';
import { AppMaterialModule } from '../app-material.module';

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
    ReactiveFormsModule,
    AppMaterialModule,
    AuthenticationRoutingModule,
  ]
})
export class AuthenticationModule { }
