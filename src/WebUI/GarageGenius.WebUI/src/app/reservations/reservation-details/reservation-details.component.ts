import { Component, OnInit } from '@angular/core';
import { ReservationService } from '../services/reservation.service';
import {ActivatedRoute, Params, Router } from '@angular/router';
import {ReservationHistoryDto, VehicleReservationHistoryModel} from '../models/vehicle-reservation-history.model';
import { VehicleReservationResponseModel } from '../models/vehicle-reservation-response.model';
import { VehiclesService } from 'src/app/vehicles/service/vehicles.service';
import { IVehiclesService } from 'src/app/vehicles/models/base-vehicle.service';
import { VehicleResponseModel } from 'src/app/vehicles/models/vehicle.model';
import {Column} from "../../shared/components/table-gg/table-gg.component";
import {UpdateReservationRequestModel} from "../models/update-reservation-request.model";
import {FormBuilder, FormGroup} from "@angular/forms";
import {UpdateReservationFormModel} from "../../pending-reservations/models/update-reservation-form.model";
import {AuthenticationService} from "../../shared/services/authentication/authentication.service";
import {UserService} from "../../users/services/user.service";
import {SnackBarMessageService} from "../../shared/services/snack-bar-message/snack-bar-message.service";

@Component({
  selector: 'app-reservation-details',
  templateUrl: './reservation-details.component.html',
  styleUrls: ['./reservation-details.component.scss'],
})
export class ReservationDetailsComponent implements OnInit {
  private readonly _reservationsService: ReservationService;
  private readonly _vehicleService: IVehiclesService;
  private readonly _activatedRoute: ActivatedRoute;
  private readonly _router: Router;
  public reservationDetails?: VehicleReservationResponseModel;
  public reservationHistory?: VehicleReservationHistoryModel;
  public vehicleDetails?: VehicleResponseModel;
  public editMode: boolean = false;
  public updateReservationForm!: FormGroup<UpdateReservationFormModel>;
  public _userInfo = this._authenticationService.getUserInfo();
  public reservationStates: Array<string> = [	"Pending", "Canceled", "Completed", "WaitingForCustomer", "Rejected", "Accepted", "WorkInProgress"];
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
    // {
    //   columnDef: 'reservationHistoryId',
    //   header: 'ID',
    //   cell: (element: ReservationHistoryDto) => `${element.reservationHistoryId}`,
    // },
    {
      columnDef: 'updateDate',
      header: 'Data aktualizacji',
      // header: 'Date',
      cell: (element: ReservationHistoryDto) => `${element.updateDate ? new Date(element.updateDate).toLocaleString() : ''}`,
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
    private _formBuilder: FormBuilder,
    private _authenticationService: AuthenticationService,
    private userService: UserService,
  ) {this._reservationsService = reservationsService;
    this._activatedRoute = activatedRoute;
    this._vehicleService = vehicleService;
    this._router = router;
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

    const userInfo = this._authenticationService.getUserInfo();
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
    this._vehicleService.getVehicleById(vehicleId).subscribe((vehicle) => {
      this.vehicleDetails = vehicle;
    });
  }

  public goBack(): void {
    this._router.navigate(['dashboard/vehicles/' + this.reservationDetails?.vehicleId]);
  }

  public updateReservation(): void {
    const updatedReservation: UpdateReservationRequestModel = {
      reservationState : this.updateReservationForm.value.reservationState!,
      reservationDate: this.updateReservationForm.value.reservationDate!,
      reservationId: this.reservationDetails?.reservationId!,
      reservationNote: this.updateReservationForm.value.comment!,
      changerId: this._userInfo?.customerId!,
    };


    this._reservationsService.updateReservation(updatedReservation)
      .subscribe((reservation) => {
        window.location.reload();
      });
  }

  public editReservation(): void {
    this.editMode = !this.editMode;

    this.updateReservationForm = this._formBuilder.group({
      reservationId: [this.reservationDetails!.reservationId],
      reservationState: [this.reservationDetails!.reservationState],
      reservationDate: [this.reservationDetails!.reservationDate],
      comment: [this.reservationDetails!.comment],
      vehicleId: [this.reservationDetails!.vehicleId]
    });
  }
}
