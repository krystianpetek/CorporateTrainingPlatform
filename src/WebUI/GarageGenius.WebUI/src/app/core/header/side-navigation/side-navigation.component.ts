import { Component, EventEmitter, Output } from '@angular/core';
import { Router } from '@angular/router';
import {
  IAuthenticationService,
  AuthenticationServiceBase,
} from 'src/app/shared/services/authentication/authentication.service';

@Component({
  selector: 'app-side-navigation',
  templateUrl: './side-navigation.component.html',
  styleUrls: ['./side-navigation.component.scss'],
})
export class SideNavigationComponent {
  private readonly _authenticationService: IAuthenticationService;
  private readonly _router: Router;

  @Output() public sideNavigationClose = new EventEmitter();

  constructor(
    authenticationService: AuthenticationServiceBase,
    router: Router
  ) {
    this._authenticationService = authenticationService;
    this._router = router;
  }

  public onCloseSideNavigation = () => {
    this.sideNavigationClose.emit();
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
    this.onCloseSideNavigation();
    this._router.navigate(['/home']);
  }
}
