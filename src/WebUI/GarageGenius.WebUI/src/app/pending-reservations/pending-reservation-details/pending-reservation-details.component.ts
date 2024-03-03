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

import {FormBuilder, FormGroup, ReactiveFormsModule} from "@angular/forms";
import {UpdateReservationFormModel} from "../models/update-reservation-form.model";
import {UserService} from "../../users/services/user.service";

@Component({
  selector: 'app-pending-reservation-details',
  standalone: true,
  imports: [CommonModule, TableGgComponent, AppMaterialModule, ReactiveFormsModule],
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
  public reservationStates: Array<string> = [	"Pending", "Canceled", "Completed", "WaitingForCustomer", "Rejected", "Accepted", "WorkInProgress"];
  public updateReservationForm!: FormGroup<UpdateReservationFormModel>;
  public reservationStatesMap: Map<string, string> = new Map([
    ["Pending", "Oczekująca na przyjęcie"],
    ["Canceled", "Anulowana"],
    ["Completed", "Zakończona"],
    ["WaitingForCustomer", "Oczekująca na decyzję klienta"],
    ["Rejected", "Odrzucona"],
    ["Accepted", "Zaakceptowana"],
    ["WorkInProgress", "W trakcie realizacji"],
  ]);

  tableColumns : Array<Column> = [
    {
      columnDef: 'reservationHistoryId',
      header: 'Identyfikator',
      cell: (element: ReservationHistoryDto) => `${element.reservationHistoryId}`,
    },
    {
      columnDef: 'updateDate',
      header: 'Data aktualizacji',
      // header: 'Date',
      cell: (element: ReservationHistoryDto) => `${element.updateDate ? new Date(element.updateDate).toLocaleDateString() : ''}`,
    },
    {
      columnDef: 'reservationState',
      // header: 'State',
      header: 'Status rezerwacji',
      cell: (element: ReservationHistoryDto) => `${this.reservationStatesMap.get(element.reservationState)}`,
    },
    {
      columnDef: 'comment',
      // header: 'Comment',
      header: 'Komentarz',
      cell: (element: ReservationHistoryDto) => `${element.comment}`,
    },

    {
      columnDef: 'changerId',
      // header: 'Comment',
      header: 'Zaktualizowane przez',
      cell: (element: ReservationHistoryDto) => `${element.userId}`,
    }
  ]

  tableData: Array<ReservationHistoryDto> = [];

  constructor(
    reservationsService: ReservationService,
    activatedRoute: ActivatedRoute,
    vehicleService: VehiclesService,
    router: Router,
    private userService: UserService,
    private _formBuilder: FormBuilder,
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
        this.tableData = reservation.reservationHistoriesDtos.map(
          (reservationHistory) => {

            this.userService.getUserById(reservationHistory.userId).subscribe((user) => {
              reservationHistory.userId = user.email ?? "GarageGenius";
            });

            reservationHistory.reservationHistoryId = reservationHistory.reservationHistoryId;
            reservationHistory.updateDate = reservationHistory.updateDate;
            reservationHistory.reservationState = reservationHistory.reservationState;
            reservationHistory.comment = reservationHistory.comment;
            return reservationHistory;
          }
        );
      });

    this.updateReservationForm = this._formBuilder.group({
      reservationId: [''],
      reservationState: [''],
      reservationDate: [new Date()],
      comment: [''],
      vehicleId: [''],
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
      reservationState : this.updateReservationForm.value.reservationState!,
      reservationDate: this.updateReservationForm.value.reservationDate!,
      reservationId: this.reservationDetails?.reservationId!,
      reservationNote: this.updateReservationForm.value.comment!,
    };


    this._reservationsService.updateReservation(updatedReservation)
      .subscribe((reservation) => {
        window.location.reload();
    });
  }

  public editReservation(): void {
    this.editMode = !this.editMode;

    // this.updateReservationForm = this._formBuilder.group({
    //   reservationId: [this.reservationDetails!.reservationId],
    //   reservationState: [this.reservationDetails!.reservationState],
    //   reservationDate: [this.reservationDetails!.reservationDate],
    //   comment: [this.reservationDetails!.comment],
    //   vehicleId: [this.reservationDetails!.vehicleId],
    // });
  }

  public goBack(): void {
    this._router.navigate(['dashboard/pending-reservations/']);
  }
}
