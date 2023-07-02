import { Component, OnInit } from '@angular/core';
import { ReservationService } from '../services/reservation.service';
import { ActivatedRoute, Params } from '@angular/router';
import { VehicleReservationHistoryModel } from '../models/vehicle-reservation-history.model';
import { VehicleReservationResponseModel } from '../models/vehicle-reservation-response.model';
import { VehiclesService } from 'src/app/vehicles/service/vehicles.service';
import { IVehiclesService } from 'src/app/vehicles/models/base-vehicle.service';
import { VehicleResponseModel } from 'src/app/vehicles/models/vehicle.model';

@Component({
  selector: 'app-reservation-details',
  templateUrl: './reservation-details.component.html',
  styleUrls: ['./reservation-details.component.scss'],
})
export class ReservationDetailsComponent implements OnInit {
  private readonly _reservationsService: ReservationService;
  private readonly _vehicleService: IVehiclesService;
  private readonly _activatedRoute: ActivatedRoute;
  public reservationDetails?: VehicleReservationResponseModel;
  public reservationHistory?: VehicleReservationHistoryModel;
  public vehicleDetails?: VehicleResponseModel;

  constructor(
    reservationsService: ReservationService,
    activatedRoute: ActivatedRoute,
    vehicleService: VehiclesService
  ) {
    this._reservationsService = reservationsService;
    this._activatedRoute = activatedRoute;
    this._vehicleService = vehicleService;
  }

  public ngOnInit(): void {
    let reservationId = '';
    this._activatedRoute.params.subscribe((params: Params) => {
      reservationId = params['id'];
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

  public getVehicleDetails(vehicleId: string): void {
    this._vehicleService.getVehicleById(vehicleId).subscribe((vehicle) => {
      this.vehicleDetails = vehicle;
    });
  }
}
