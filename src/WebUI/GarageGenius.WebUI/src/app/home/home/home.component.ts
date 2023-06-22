import { Component } from '@angular/core';
import { Router } from '@angular/router';
import {
  AuthenticationServiceBase,
  IAuthenticationService,
} from 'src/app/shared/services/authentication/authentication.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent {
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
