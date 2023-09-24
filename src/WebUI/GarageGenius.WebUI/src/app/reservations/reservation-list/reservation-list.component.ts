import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { CustomerReservationsItem } from '../models/customer-reservations-response.model';
import {
  AuthenticationService,
  IAuthenticationService,
} from 'src/app/shared/services/authentication/authentication.service';
import { IReservationService } from '../models/base-reservation.service';
import { ReservationService } from '../services/reservation.service';
import { MatDialog } from "@angular/material/dialog";
import { ReservationAddComponent } from "../reservation-add/reservation-add.component";

@Component({
  selector: 'app-reservation-list',
  templateUrl: './reservation-list.component.html',
  styleUrls: ['./reservation-list.component.scss'],
})
export class ReservationListComponent implements OnInit {
  private _router: Router;
  private _authenticationService: IAuthenticationService;
  private _reservationService: IReservationService;

  public dataSource = new MatTableDataSource<CustomerReservationsItem>();
  public displayedColumns: string[] = [
    'details',
    `vehicleId`,
    `vehicleName`,
    `reservationId`,
    `reservationState`,
    `reservationDate`,
    `comment`,
  ];

  public constructor(
    router: Router,
    authenticationService: AuthenticationService,
    reservationService: ReservationService,
    public dialog: MatDialog,
  ) {
    this._router = router;
    this._authenticationService = authenticationService;
    this._reservationService = reservationService;
  }

  ngOnInit(): void {
    const user = this._authenticationService.getUserInfo();

    this._reservationService
      .getCustomerReservations(user.customerId)
      .subscribe((reservations) => {
        this.dataSource.data = reservations.customerReservationsDto.items;
      });
  }

  public redirectToDetails(reservationId: string): void {
    this._router.navigate([`dashboard/reservations`, reservationId]);
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(ReservationAddComponent, {
      data: {
        customerId: this._authenticationService.getUserInfo().customerId
      },
      // TODO response after close
    });
  }

}
