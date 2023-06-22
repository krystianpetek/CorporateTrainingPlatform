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
export class LayoutComponent {
  private readonly _authenticationService: IAuthenticationService;
  private readonly _router: Router;

  constructor(
    authenticationService: AuthenticationServiceBase,
    router: Router
  ) {
    this._authenticationService = authenticationService;
    this._router = router;
  }

  public isUserLoggedIn(): boolean {
    const user = this._authenticationService.getUserInfo();
    if (user) {
      return true;
    }
    return false;
  }

  public signOut(): void {
    this._authenticationService.signOutUser();

    this._router.navigate(['/home']);
  }
}
