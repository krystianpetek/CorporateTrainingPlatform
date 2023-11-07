import {Component} from '@angular/core';
import {
  AuthenticationService
} from "../../shared/services/authentication/authentication.service";
import {Role} from "../../shared/services/authentication/models/role.model";

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent {

  public constructor(private _authenticationService: AuthenticationService) { }

  public get isAuthenticated(): boolean
  {
    return this._authenticationService.getUserInfo().role !== Role.Customer;
  }

}
