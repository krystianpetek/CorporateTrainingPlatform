import {Component, OnDestroy, OnInit} from '@angular/core';
import {
  AuthenticationService
} from "../../shared/services/authentication/authentication.service";
import {Role} from "../../shared/services/authentication/models/role.model";
import {SignalRService} from "../../shared/services/signalr/signal-r.service";

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit, OnDestroy {

  public userName?: string;
  public constructor(
    private _authenticationService: AuthenticationService,
    public _signalRService: SignalRService) { }

  public get isAuthenticated(): boolean
  {
    return this._authenticationService.getUserInfo().role !== Role.Customer;
  }

  ngOnInit(): void {
    this.userName = this._authenticationService.getUserInfo().email;
    this._signalRService.createHubConnection();
    this._signalRService.registerHandlers();
  }

  ngOnDestroy(): void {
    this._signalRService.stopHubConnection();
  }
}
