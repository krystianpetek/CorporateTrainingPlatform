import { Component, OnInit } from '@angular/core';
import { VehiclesService as VehicleService } from '../service/vehicles.service';
import { ActivatedRoute, Router } from '@angular/router';
import { VehicleResponseModel } from '../models/vehicle.model';
import { VehicleReservationsComponent } from '../vehicle-reservations/vehicle-reservations.component';
import { MatDialog } from '@angular/material/dialog';
import { SnackBarMessageService } from 'src/app/shared/services/snack-bar-message/snack-bar-message.service';

@Component({
  selector: 'app-vehicle-details',
  templateUrl: './vehicle-details.component.html',
  styleUrls: ['./vehicle-details.component.scss'],
})
export class VehicleDetailsComponent implements OnInit {
  private readonly _vehicleService: VehicleService;
  private readonly _activatedRoute: ActivatedRoute;
  private readonly _router: Router;
  private readonly _dialog: MatDialog;
  public vehicleResponse?: VehicleResponseModel;
  public editMode: boolean;
  private readonly _snackbarService: SnackBarMessageService;
  public vehicleId: string;

  constructor(
    vehicleService: VehicleService,
    activatedRoute: ActivatedRoute,
    router: Router,
    dialog: MatDialog,
    snackbarService: SnackBarMessageService
  ) {
    this._vehicleService = vehicleService;
    this._activatedRoute = activatedRoute;
    this._router = router;
    this._dialog = dialog;
    this.editMode = false;
    this._snackbarService = snackbarService;
    this.vehicleId = '';
  }
  ngOnInit(): void {
    this._activatedRoute.params.subscribe((params) => {
      this.vehicleId = params['id'];
      this.vehicle(this.vehicleId);
    });
  }

  public vehicle(id: string) {
    return this._vehicleService.getVehicleById(id).subscribe((vehicle) => {
      this.vehicleResponse = vehicle;
    });
  }

  public goBack(): void {
    this._router.navigate(['dashboard/vehicles']);
  }

  openDialog(): void {
    const dialogRef = this._dialog.open(VehicleReservationsComponent, {
      data: {
        vehicleId: this.vehicleResponse?.id,
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      // todo - refresh vehicle details etc
    });
  }

  public editVehicle(): void {
    this.editMode = !this.editMode;
  }

  public saveVehicle(): void {
    this._snackbarService.fail('Not implemented yet!', 2);
  }
}
