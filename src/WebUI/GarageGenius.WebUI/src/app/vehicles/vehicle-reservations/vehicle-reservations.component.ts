import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ReservationsService } from 'src/app/reservations/services/reservations.service';
import { VehicleReservationsModalProperties } from './models/vehicle-reservations-modal-properties.model';
import { VehicleReservationsResponseModel } from 'src/app/reservations/models/vehicle-reservations-response.model';

@Component({
  selector: 'app-vehicle-reservations',
  templateUrl: './vehicle-reservations.component.html',
  styleUrls: ['./vehicle-reservations.component.scss'],
})
export class VehicleReservationsComponent implements OnInit {
  private readonly _reservationsService: ReservationsService;
  private readonly dialogRef: MatDialogRef<VehicleReservationsComponent>;
  public vehicleReservationsResponse?: VehicleReservationsResponseModel;

  constructor(
    reservationsService: ReservationsService,
    dialogRef: MatDialogRef<VehicleReservationsComponent>,
    @Inject(MAT_DIALOG_DATA)
    public matDialogData: VehicleReservationsModalProperties
  ) {
    this._reservationsService = reservationsService;
    this.dialogRef = dialogRef;
  }

  ngOnInit(): void {
    this.getVehicleReservations(this.matDialogData.vehicleId);
  }

  private getVehicleReservations(vehicleId: string) {
    this._reservationsService
      .getVehicleReservations(vehicleId)
      .subscribe((reservations) => {
        this.vehicleReservationsResponse = reservations;
        console.log(this.vehicleReservationsResponse);
      });
  }
}
