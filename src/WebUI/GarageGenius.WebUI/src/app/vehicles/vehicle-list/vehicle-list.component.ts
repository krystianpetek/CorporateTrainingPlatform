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

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.scss'],
})
export class VehicleListComponent implements OnInit {
  private _vehiclesService: IVehiclesService;
  private _authenticationService: IAuthenticationService;
  private _router: Router;
  public dataSource = new MatTableDataSource<VehicleResponseModel>();
  public displayedColumns: string[] = [
    'id',
    'details',
    'manufacturer',
    'model',
    'licensePlate',
    'vin',
    'year',
    // 'update',
    // 'delete',
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
        this.dataSource.data = vehicles;
      });
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(VehicleAddComponent, {
      data: {
        customerId: this._authenticationService.getUserInfo().customerId,
        panelClass: 'classas',
      },
    });

    dialogRef.afterClosed().subscribe((vehicle) => {
      if (!vehicle) {
        return;
      }
      const currentUrl = this._router.url;
      this._router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
        this._router.navigate([currentUrl]);
      });

      // TODO - change handling of this to refresh the list of vehicles ?
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
