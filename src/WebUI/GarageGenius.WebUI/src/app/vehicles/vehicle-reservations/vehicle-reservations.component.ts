import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ReservationService } from 'src/app/reservations/services/reservation.service';
import { VehicleReservationsModalProperties } from './models/vehicle-reservations-modal-properties.model';
import { VehicleReservationsResponseModel } from 'src/app/reservations/models/vehicle-reservations-response.model';
import { Router } from '@angular/router';
import {ReservationAddComponent} from "../../reservations/reservation-add/reservation-add.component";
import {AuthenticationService} from "../../shared/services/authentication/authentication.service";

@Component({
  selector: 'app-vehicle-reservations',
  templateUrl: './vehicle-reservations.component.html',
  styleUrls: ['./vehicle-reservations.component.scss'],
})
export class VehicleReservationsComponent implements OnInit {
  private readonly _reservationsService: ReservationService;
  private readonly _authenticationService: AuthenticationService;
  private readonly _router: Router;
  public vehicleReservationsResponse?: VehicleReservationsResponseModel;
  @Input() public vehicleId: string;
  private readonly matDialog: MatDialog;
  private readonly reservationStatesMap: Map<string, string> = new Map([
    ['Pending', 'Oczekująca'],
    ['Canceled', 'Anulowana'],
    ['Completed', 'Zakończona'],
    ['WaitingForCustomer', 'Oczekująca na klienta'],
    ['Rejected', 'Odrzucona'],
    ['Accepted', 'Zaakceptowana'],
    ['WorkInProgress', 'W trakcie realizacji'],
  ]);
  constructor(
    reservationsService: ReservationService,
    authenticationService: AuthenticationService,
    router: Router,
    matDialog: MatDialog
  ) {
    this._reservationsService = reservationsService;
    this._authenticationService = authenticationService;
    this._router = router;
    this.vehicleId = '';
    this.matDialog = matDialog
  }

  ngOnInit(): void {
    this.getVehicleReservations(this.vehicleId);
  }
  // TODO - spinner or something while waiting for response

  public redirectToReservationDetails(reservationId: string) {
    this._router.navigate(['dashboard/reservations', reservationId]);
  }

  public addNewReservation() {
    const dialogRef = this.matDialog.open(ReservationAddComponent, {
      data: {
        vehicleId: this.vehicleId,
        customerId: this._authenticationService.getUserInfo().customerId,
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      // todo - refresh vehicle details etc
    });
  }

  private getVehicleReservations(vehicleId: string) {
    this._reservationsService
      .getVehicleReservations(vehicleId)
      .subscribe((reservations) => {
        this.vehicleReservationsResponse = reservations
        this.vehicleReservationsResponse.vehicleReservationsDto.forEach((vehicleReservationDto) => {
          vehicleReservationDto.reservationState = this.reservationStatesMap.get(vehicleReservationDto.reservationState)!;
        });
      });
  }
}
