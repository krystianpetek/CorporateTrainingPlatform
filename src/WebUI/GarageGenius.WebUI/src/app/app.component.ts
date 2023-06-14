import { Component } from '@angular/core';
import {
  AuthenticationServiceBase,
  IAuthenticationService,
} from './shared/services/authentication/authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  private readonly _authenticationService: IAuthenticationService;
  constructor(authenticationService: AuthenticationServiceBase) {
    this._authenticationService = authenticationService;
  }

  public isUserLogggedIn(): boolean {
    return this._authenticationService.getAuthenticationToken() !== null;
  }
}
