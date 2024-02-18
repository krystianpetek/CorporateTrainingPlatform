import { Component, EventEmitter, Output } from '@angular/core';
import { Router } from '@angular/router';
import {
  AuthenticationServiceBase,
  IAuthenticationService,
} from 'src/app/shared/services/authentication/authentication.service';
import {Role} from "../../shared/services/authentication/models/role.model";

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent {
  private readonly _authenticationService: IAuthenticationService;
  private readonly _router: Router;

  @Output() public sideNavigationToggle = new EventEmitter();

  constructor(
    authenticationService: AuthenticationServiceBase,
    router: Router
  ) {
    this._authenticationService = authenticationService;
    this._router = router;
  }

  public onToggleSideNavigation = () => {
    this.sideNavigationToggle.emit();
  };

  public isUserLoggedIn(): boolean {
    const user = this._authenticationService.getUserInfo();
    if (user) {
      return true;
    }
    return false;
  }

  public isAdmin(): boolean {
    let user = this._authenticationService.getUserInfo()?.role === Role.Administrator;
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
