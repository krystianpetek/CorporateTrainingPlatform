import { Component, Inject, Input, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ReservationService } from 'src/app/reservations/services/reservation.service';
import { VehicleReservationsModalProperties } from './models/vehicle-reservations-modal-properties.model';
import { VehicleReservationsResponseModel } from 'src/app/reservations/models/vehicle-reservations-response.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-vehicle-reservations',
  templateUrl: './vehicle-reservations.component.html',
  styleUrls: ['./vehicle-reservations.component.scss'],
})
export class VehicleReservationsComponent implements OnInit {
  private readonly _reservationsService: ReservationService;
  private readonly _router: Router;
  public vehicleReservationsResponse?: VehicleReservationsResponseModel;
  @Input() public vehicleId: string;
  // private readonly dialogRef: MatDialogRef<VehicleReservationsComponent>;

  constructor(
    reservationsService: ReservationService,
    router: Router
    // dialogRef: MatDialogRef<VehicleReservationsComponent>,
    // @Inject(MAT_DIALOG_DATA)
    // public matDialogData: VehicleReservationsModalProperties
  ) {
    this._reservationsService = reservationsService;
    this._router = router;
    this.vehicleId = '';
    // this.dialogRef = dialogRef;
  }

  ngOnInit(): void {
    this.getVehicleReservations(this.vehicleId);
  }
  // TODO - spinner or something while waiting for response

  public redirectToReservationDetails(reservationId: string) {
    this._router.navigate(['dashboard/reservations', reservationId]);
    // this.dialogRef.close();
  }

  public addNewReservation() {
    this._router.navigate(['dashboard/reservations/add'], {
      queryParams: { vehicleId: this.vehicleId },
    });
    // this.dialogRef.close();
  }

  private getVehicleReservations(vehicleId: string) {
    this._reservationsService
      .getVehicleReservations(vehicleId)
      .subscribe((reservations) => {
        this.vehicleReservationsResponse = reservations;
      });
  }
}
