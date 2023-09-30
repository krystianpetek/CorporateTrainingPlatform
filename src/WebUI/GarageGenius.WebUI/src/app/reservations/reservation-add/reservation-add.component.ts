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
  private _isSuccess: boolean;

  constructor(
    private snackbar: SnackBarMessageService,
    formBuilder: FormBuilder,
    reservationService: ReservationService,
    dialogRef: MatDialogRef<ReservationAddComponent>,
    @Inject(MAT_DIALOG_DATA) public matDialogData: ReservationAddModalProperties
  ) {
    this._formBuilder = formBuilder;
    this._reservationService = reservationService;
    this._dialogRef = dialogRef;
    this.error = '';
    this._isSuccess = true;
  }

  public reservationAddForm!: FormGroup<ReservationAddFormModel>;

  public ngOnInit(): void {
    this.reservationAddForm = this._formBuilder.group({
      customerId: [
        this.matDialogData.customerId,
        {
          validators: [],
          nonNullable: false,
        },
      ],
      vehicleId:[
        this.matDialogData.vehicleId,
        {
          validators: [Validators.required],
          nonNullable: false
        }
      ],
      comment:[
        ""
      ],
      reservationDate:[
        new Date()
      ],
      reservationState:[
        "s"
      ],
    })
  }

  public reservationAddSubmitForm(): void {
    this.error = ``;
    const reservationAddModel: ReservationAddRequestModel = this.reservationAddForm.value as ReservationAddRequestModel;
    // TODO request to add reservation

    this._reservationService.addReservation(reservationAddModel).subscribe(
      {
        next: (response) => {
          this._dialogRef.close(reservationAddModel);
          window.location.reload();
        },
        error: (err) => {
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
