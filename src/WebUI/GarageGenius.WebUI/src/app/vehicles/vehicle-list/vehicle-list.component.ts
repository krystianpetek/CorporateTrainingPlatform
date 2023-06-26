import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';

import { VehicleResponseModel } from '../models/vehicle.model';
import { VehiclesService } from '../service/vehicles.service';
import { IVehiclesService } from '../models/base-vehicle.service';
import {
  AuthenticationService,
  IAuthenticationService,
} from 'src/app/shared/services/authentication/authentication.service';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { VehicleAddComponent } from '../vehicle-add/vehicle-add.component';
import { ScrollStrategyOptions } from '@angular/cdk/overlay';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.scss'],
})
export class VehicleListComponent implements OnInit {
  private _vehiclesService: IVehiclesService;
  private _authenticationService: IAuthenticationService;
  private _router: Router;
  public vehicles?: Array<VehicleResponseModel>;
  public dataSource = new MatTableDataSource<VehicleResponseModel>();
  public displayedColumns: string[] = [
    'id',
    'manufacturer',
    'model',
    'year',
    'vin',
    'licensePlate',
    'details',
    'update',
    'delete',
  ];

  public constructor(
    vehiclesService: VehiclesService,
    authenticationService: AuthenticationService,
    router: Router,
    public dialog: MatDialog
  ) {
    this._vehiclesService = vehiclesService;
    this._authenticationService = authenticationService;
    this._router = router;
  }

  ngOnInit(): void {
    const user = this._authenticationService.getUserInfo();

    this._vehiclesService
      .getCustomerVehicles(user.customerId)
      .subscribe((vehicles) => {
        this.vehicles = vehicles;
        this.dataSource.data = vehicles as Array<VehicleResponseModel>;
      });
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(VehicleAddComponent, {
      data: {
        customerId: this._authenticationService.getUserInfo().customerId,
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      console.log('The dialog was closed');
      console.log(result);
    });
  }

  public redirectToDetails = (id: string) => {
    this._router.navigate(['dashboard/vehicles/', id]);
    console.log(`details ${id}`);
  };
  public redirectToUpdate = (id: string) => {
    console.log(`update ${id}`);
  };
  public redirectToDelete = (id: string) => {
    console.log(`delete ${id}`);
  };
}
