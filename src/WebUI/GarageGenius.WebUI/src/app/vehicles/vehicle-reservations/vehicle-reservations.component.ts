import { Component, Inject, OnInit } from '@angular/core';
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
  private readonly dialogRef: MatDialogRef<VehicleReservationsComponent>;
  private readonly _router: Router;
  public vehicleReservationsResponse?: VehicleReservationsResponseModel;

  constructor(
    reservationsService: ReservationService,
    dialogRef: MatDialogRef<VehicleReservationsComponent>,
    router: Router,
    @Inject(MAT_DIALOG_DATA)
    public matDialogData: VehicleReservationsModalProperties
  ) {
    this._reservationsService = reservationsService;
    this.dialogRef = dialogRef;
    this._router = router;
  }

  ngOnInit(): void {
    this.getVehicleReservations(this.matDialogData.vehicleId);
  }
  // TODO - spinner or something while waiting for response

  public redirectToReservationDetails(reservationId: string) {
    this._router.navigate(['dashboard/reservations', reservationId], {
      queryParams: { reservationId: reservationId },
    });
    this.dialogRef.close();
  }

  public addNewReservation() {
    this._router.navigate(['dashboard/reservations/add'], {
      queryParams: { vehicleId: this.matDialogData.vehicleId },
    });
    this.dialogRef.close();
  }

  private getVehicleReservations(vehicleId: string) {
    this._reservationsService
      .getVehicleReservations(vehicleId)
      .subscribe((reservations) => {
        this.vehicleReservationsResponse = reservations;
      });
  }
}
