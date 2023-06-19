import { Component, OnInit } from '@angular/core';
import { VehicleModel } from '../models/vehicle.model';
import { VehiclesService } from '../service/vehicles.service';
import { IVehiclesService } from '../models/base-vehicle.service';

@Component({
  selector: 'app-vehicles',
  templateUrl: './vehicles.component.html',
  styleUrls: ['./vehicles.component.scss'],
})
export class VehiclesComponent implements OnInit {
  private _vehiclesService: IVehiclesService;
  public vehicles?: Array<VehicleModel>;
  public errorMessage?: string;

  public constructor(vehiclesService: VehiclesService) {
    this._vehiclesService = vehiclesService;
  }

  ngOnInit(): void {
    this._vehiclesService
      .getCustomerVehicles(`4d5bd340-2728-4dab-9d9a-ebd7962bd43e`)
      .subscribe((vehicles) => (this.vehicles = vehicles));
  }
}
