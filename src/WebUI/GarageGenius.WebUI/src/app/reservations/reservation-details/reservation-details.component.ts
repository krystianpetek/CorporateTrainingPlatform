import { Component, OnInit } from '@angular/core';
import { ReservationsService } from '../services/reservations.service';
import { ActivatedRoute, Params } from '@angular/router';

@Component({
  selector: 'app-reservation-details',
  templateUrl: './reservation-details.component.html',
  styleUrls: ['./reservation-details.component.scss'],
})
export class ReservationDetailsComponent implements OnInit {
  private readonly _reservationsService: ReservationsService;
  private readonly _activatedRoute: ActivatedRoute;

  constructor(
    reservationsService: ReservationsService,
    activatedRoute: ActivatedRoute
  ) {
    this._reservationsService = reservationsService;
    this._activatedRoute = activatedRoute;
  }

  public ngOnInit(): void {
    let reservationId = '';
    this._activatedRoute.queryParams.subscribe((params: Params) => {
      reservationId = params['reservationId'];
    });

    this._reservationsService
      .getReservationHistory(reservationId)
      .subscribe((reservation) => {
        reservationId = reservation.reservationId;
      });
    console.log(reservationId);
  }
}
