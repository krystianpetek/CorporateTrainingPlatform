import { CommonModule } from '@angular/common';

import { Component, OnInit } from '@angular/core';

import {ActivatedRoute, Params, Router } from '@angular/router';
import { VehiclesService } from 'src/app/vehicles/service/vehicles.service';
import { IVehiclesService } from 'src/app/vehicles/models/base-vehicle.service';
import { VehicleResponseModel } from 'src/app/vehicles/models/vehicle.model';
import {Column, TableGgComponent} from "../../shared/components/table-gg/table-gg.component";
import {ReservationService} from "../../reservations/services/reservation.service";
import {VehicleReservationResponseModel} from "../../reservations/models/vehicle-reservation-response.model";
import {
  ReservationHistoryDto,
  VehicleReservationHistoryModel
} from "../../reservations/models/vehicle-reservation-history.model";
import {AppMaterialModule} from "../../shared/app-material.module";
import {UpdateReservationRequestModel} from "../../reservations/models/update-reservation-request.model";

@Component({
  selector: 'app-pending-reservation-details',
  standalone: true,
  imports: [CommonModule, TableGgComponent, AppMaterialModule],
  templateUrl: './pending-reservation-details.component.html',
  styleUrl: './pending-reservation-details.component.scss'
})
export class PendingReservationDetailsComponent implements OnInit {
  private readonly _reservationsService: ReservationService;
  private readonly _vehicleService: IVehiclesService;
  private readonly _activatedRoute: ActivatedRoute;
  private readonly _router: Router;
  public editMode: boolean;
  public reservationDetails?: VehicleReservationResponseModel;
  public reservationHistory?: VehicleReservationHistoryModel;
  public vehicleDetails?: VehicleResponseModel;

  tableColumns : Array<Column> = [
    {
      columnDef: 'reservationHistoryId',
      header: 'ID',
      cell: (element: ReservationHistoryDto) => `${element.reservationHistoryId}`,
    },
    {
      columnDef: 'updateDate',
      header: 'Date',
      cell: (element: ReservationHistoryDto) => `${element.updateDate}`,
    },
    {
      columnDef: 'reservationState',
      header: 'State',
      cell: (element: ReservationHistoryDto) => `${element.reservationState}`,
    },
    {
      columnDef: 'comment',
      header: 'Comment',
      cell: (element: ReservationHistoryDto) => `${element.comment}`,
    },
    {
      columnDef: 'user',
      header: 'User',
      cell: (element: ReservationHistoryDto) => `${element.userId}`,
    }
  ]

  tableData: Array<ReservationHistoryDto> = [];

  constructor(
    reservationsService: ReservationService,
    activatedRoute: ActivatedRoute,
    vehicleService: VehiclesService,
    router: Router,
  ) {this._reservationsService = reservationsService;
    this._activatedRoute = activatedRoute;
    this._vehicleService = vehicleService;
    this._router = router;
    this.editMode = false;
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
        this.tableData = reservation.reservationHistoriesDtos;
      });
  }

  public getVehicleDetails(vehicleId: string): void {
    this._vehicleService.getVehicleById(vehicleId)
      .subscribe((vehicle) => {
        this.vehicleDetails = vehicle;
    });
  }

  public updateReservation(): void {
    const updatedReservation: UpdateReservationRequestModel = {
      reservationId: this.reservationDetails!.reservationId,
      reservationDate: this.reservationDetails!.reservationDate,
      reservationNote: this.reservationDetails!.comment,
      reservationState: this.reservationDetails!.reservationState
    }
    this._reservationsService.updateReservation(updatedReservation)
      .subscribe((reservation) => {
    });
  }

  public editReservation(): void {
    this.editMode = !this.editMode;
  }

  public goBack(): void {
    this._router.navigate(['dashboard/vehicles/' + this.reservationDetails?.vehicleId]);
  }
}
