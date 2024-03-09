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
import {MatRadioModule} from "@angular/material/radio";
import {FormsModule} from "@angular/forms";

@Component({
  selector: 'app-pending-reservation-list',
  standalone: true,
  imports: [CommonModule, MatButtonModule, MatIconModule, MatTableModule, MatRadioModule, FormsModule],
  templateUrl: './pending-reservation-list.component.html',
  styleUrl: './pending-reservation-list.component.scss'
})
export class PendingReservationListComponent implements OnInit {
  private _router: Router;
  private _authenticationService: IAuthenticationService;
  private _reservationService: IReservationService;
  showMode: 'toDecision' | 'pending' = 'pending';

  public dataSource = new MatTableDataSource<CustomerReservationsItem>();
  public displayedColumns: string[] = [
    'details',
    `vehicleId`,
    `reservationId`,
    `reservationState`,
    `reservationDate`,
    `comment`,
  ];

  private readonly reservationStatesMap: Map<string, string> = new Map([
    ['Pending', 'Oczekująca'],
    ['Canceled', 'Anulowana'],
    ['Completed', 'Zakończona'],
    ['WaitingForCustomer', 'Oczekująca na klienta'],
    ['Rejected', 'Odrzucona'],
    ['Accepted', 'Zaakceptowana'],
    ['WorkInProgress', 'W trakcie realizacji'],
  ]);

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
      .getNotCompletedReservations(this.showMode === 'toDecision')
      .subscribe((reservations) => {
        this.dataSource.data = reservations.currentNotCompletedReservationsDtos.items;

        this.dataSource.data = this.dataSource.data.map((reservation) => {
          reservation.reservationState = this.reservationStatesMap.get(
            reservation.reservationState
          )!;
          return reservation;
        });
      });
  }

  changedValue(event: Event): void {
    const user = this._authenticationService.getUserInfo();

    this.ngOnInit();
  }

  public redirectToDetails(reservationId: string): void {
    this._router.navigate([`dashboard/pending-reservations`, reservationId]);
  }
}
