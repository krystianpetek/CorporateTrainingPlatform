import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ReservationAddFormModel} from "../models/reservation-add-form.model";
import {ReservationAddModalProperties} from "../models/reservation-add-modal-properties.model";
import {
  ReservationAddRequestModel,
} from "../models/vehicle-reservation-response.model";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import {IReservationService} from "../models/base-reservation.service";
import {ReservationService} from "../services/reservation.service";
import {SnackBarMessageService} from "../../shared/services/snack-bar-message/snack-bar-message.service";
import {VehiclesService} from "../../vehicles/service/vehicles.service";
import {VehicleResponseModel} from "../../vehicles/models/vehicle.model";
import {AuthenticationService} from "../../shared/services/authentication/authentication.service";

@Component({
  selector: 'app-reservation-add',
  templateUrl: './reservation-add.component.html',
  styleUrls: ['./reservation-add.component.scss'],
})
export class ReservationAddComponent implements OnInit {
  private readonly _formBuilder: FormBuilder;
  private readonly _dialogRef: MatDialogRef<ReservationAddComponent>;
  private readonly _reservationService: IReservationService;
  private _vehicleId = "";
  public error: string;
  public isSuccess: boolean;
  public reservationStates: Array<string> = [	"Pending", "Canceled", "Completed", "WaitingForCustomer", "Rejected", "Accepted", "WorkInProgress"];
  public customerVehicles: Array<VehicleResponseModel> = [];
  public minDate = new Date(new Date().getTime() + 86400000);
  public readonly reservationStatesMap: Map<string, string> = new Map([
    ['Pending', 'Oczekująca'],
    ['Canceled', 'Anulowana'],
    ['Completed', 'Zakończona'],
    ['WaitingForCustomer', 'Oczekująca na klienta'],
    ['Rejected', 'Odrzucona'],
    ['Accepted', 'Zaakceptowana'],
    ['WorkInProgress', 'W trakcie realizacji'],
  ]);

  constructor(
    private snackbar: SnackBarMessageService,
    formBuilder: FormBuilder,
    reservationService: ReservationService,
    dialogRef: MatDialogRef<ReservationAddComponent>,
    private _vehiclesService: VehiclesService,
    private _authenticationService: AuthenticationService,
    @Inject(MAT_DIALOG_DATA) public matDialogData: ReservationAddModalProperties
  ) {
    this._formBuilder = formBuilder;
    this._reservationService = reservationService;
    this._dialogRef = dialogRef;
    this.error = '';
    this.isSuccess = true;
  }

  public reservationAddForm!: FormGroup<ReservationAddFormModel>;

  public ngOnInit(): void {
    const user = this._authenticationService.getUserInfo();

    this._vehiclesService
      .getCustomerVehicles(user.customerId)
      .subscribe((vehicles) => {
        this.customerVehicles = vehicles;
      });

    this.reservationAddForm = this._formBuilder.group({
      customerId: [
        this.matDialogData.customerId,
        {
          validators: [],
          nonNullable: false,
        },
      ],
      vehicleId:[
        this.matDialogData.vehicleId ?? this.customerVehicles[0]?.id,
        {
          validators: [Validators.required],
          nonNullable: false
        }
      ],
      comment:[
        "Samochód nie odpala"
      ],
      reservationDate:[
        this.minDate,
      ],
      reservationState:[
        "Pending",
        {
          validators: [Validators.required],
          nonNullable: false
        }
      ],
    })
  }

  public reservationAddSubmitForm(): void {
    this.error = ``;
    this.isSuccess = true;
    let reservationAddModel: ReservationAddRequestModel = this.reservationAddForm.value as ReservationAddRequestModel;

    reservationAddModel.reservationDate = new Date(reservationAddModel.reservationDate.getTime() + 3600000);
    // TODO request to add reservation

    this._reservationService.addReservation(reservationAddModel).subscribe(
      {
        next: (response) => {
          this._dialogRef.close(reservationAddModel);
          window.location.reload();
        },
        error: (err) => {
          this.isSuccess = false;
          let error = Object.values(err.error.errors)[0] as Array<string>;
          this.error = error[0] as string;
        }
      });
  }

  public reservationAddResetForm(): void {
    this.reservationAddForm.reset();
  }

  public get customerId(): ReservationAddFormModel['customerId'] {
    return this.reservationAddForm.controls.customerId;
  }
  public get vehicleId(): ReservationAddFormModel['vehicleId'] {
    return this.reservationAddForm.controls.vehicleId;
  }
  public get comment(): ReservationAddFormModel['comment'] {
    return this.reservationAddForm.controls.comment;
  }
  public get reservationState(): ReservationAddFormModel['reservationState'] {
    return this.reservationAddForm.controls.reservationState;
  }
  public get reservationDate(): ReservationAddFormModel['reservationDate'] {
    return this.reservationAddForm.controls.reservationDate;
  }
}
