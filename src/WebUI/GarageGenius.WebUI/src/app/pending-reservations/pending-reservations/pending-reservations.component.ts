import {Component, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import {MatTableDataSource, MatTableModule} from "@angular/material/table";
import {
  AuthenticationService,
  IAuthenticationService
} from "../../shared/services/authentication/authentication.service";
import {Router} from "@angular/router";
import {IReservationService} from "../../reservations/models/base-reservation.service";
import {CustomerReservationsItem} from "../../reservations/models/customer-reservations-response.model";
import {ReservationService} from "../../reservations/services/reservation.service";
import {MatDialog} from "@angular/material/dialog";
import {ReservationAddComponent} from "../../reservations/reservation-add/reservation-add.component";

@Component({
  selector: 'app-pending-reservations',
  standalone: true,
  imports: [CommonModule, MatButtonModule, MatIconModule, MatTableModule],
  templateUrl: './pending-reservations.component.html',
  styleUrl: './pending-reservations.component.scss'
})
export class PendingReservationsComponent implements OnInit {
  private _router: Router;
  private _authenticationService: IAuthenticationService;
  private _reservationService: IReservationService;

  public dataSource = new MatTableDataSource<CustomerReservationsItem>();
  public displayedColumns: string[] = [
    'details',
    `vehicleId`,
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
    this._reservationService
      .getNotCompletedReservations()
      .subscribe((reservations) => {
        this.dataSource.data = reservations.currentNotCompletedReservationsDtos.items;
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
