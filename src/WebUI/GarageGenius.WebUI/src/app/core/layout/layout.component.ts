import { Component } from '@angular/core';
import {
  AuthenticationServiceBase,
  IAuthenticationService,
} from '../../shared/services/authentication/authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss'],
})
export class LayoutComponent {}
