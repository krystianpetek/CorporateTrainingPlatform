import { Component, EventEmitter, Output } from '@angular/core';
import { Router } from '@angular/router';
import {
  AuthenticationServiceBase,
  IAuthenticationService,
} from 'src/app/shared/services/authentication/authentication.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent {
  private readonly _authenticationService: IAuthenticationService;
  private readonly _router: Router;

  @Output() public sidenavToggle = new EventEmitter();

  constructor(
    authenticationService: AuthenticationServiceBase,
    router: Router
  ) {
    this._authenticationService = authenticationService;
    this._router = router;
  }

  public onToggleSidenav = () => {
    this.sidenavToggle.emit();
  };

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
