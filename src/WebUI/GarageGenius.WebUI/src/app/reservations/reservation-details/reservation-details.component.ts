import { Component, OnInit } from '@angular/core';
import { ReservationService } from '../services/reservation.service';
import { ActivatedRoute, Params } from '@angular/router';
import { VehicleReservationHistoryModel } from '../models/vehicle-reservation-history.model';
import { VehicleReservationResponseModel } from '../models/vehicle-reservation-response.model';

@Component({
  selector: 'app-reservation-details',
  templateUrl: './reservation-details.component.html',
  styleUrls: ['./reservation-details.component.scss'],
})
export class ReservationDetailsComponent implements OnInit {
  private readonly _reservationsService: ReservationService;
  private readonly _activatedRoute: ActivatedRoute;
  public reservationDetails?: VehicleReservationResponseModel;
  public reservationHistory?: VehicleReservationHistoryModel;

  constructor(
    reservationsService: ReservationService,
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
      .getReservationById(reservationId)
      .subscribe((reservation) => {
        this.reservationDetails = reservation;
      });

    this._reservationsService
      .getReservationHistory(reservationId)
      .subscribe((reservation) => {
        this.reservationHistory = reservation;
      });
  }
}
