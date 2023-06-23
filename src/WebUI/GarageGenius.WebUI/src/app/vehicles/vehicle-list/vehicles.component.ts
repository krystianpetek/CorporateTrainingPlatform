import { Component, OnInit } from '@angular/core';
import { VehicleResponseModel } from '../models/vehicle.model';
import { VehiclesService } from '../service/vehicles.service';
import { IVehiclesService } from '../models/base-vehicle.service';
import {
  AuthenticationService,
  IAuthenticationService,
} from 'src/app/shared/services/authentication/authentication.service';

@Component({
  selector: 'app-vehicles',
  templateUrl: './vehicles.component.html',
  styleUrls: ['./vehicles.component.scss'],
})
export class VehiclesComponent implements OnInit {
  private _vehiclesService: IVehiclesService;
  private _authenticationService: IAuthenticationService;
  public vehicles?: Array<VehicleResponseModel>;
  public errorMessage?: string;

  public constructor(
    vehiclesService: VehiclesService,
    authenticationService: AuthenticationService
  ) {
    this._vehiclesService = vehiclesService;
    this._authenticationService = authenticationService;
  }

  ngOnInit(): void {
    const user = this._authenticationService.getUserInfo();

    this._vehiclesService
      .getCustomerVehicles(user.customerId)
      .subscribe((vehicles) => (this.vehicles = vehicles));
  }
}
