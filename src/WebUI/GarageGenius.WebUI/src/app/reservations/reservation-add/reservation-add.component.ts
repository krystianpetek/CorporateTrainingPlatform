import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ReservationAddFormModel} from "../models/reservation-add-form.model";
import {ReservationAddModalProperties} from "../models/reservation-add-modal-properties.model";
import {
  ReservationAddRequestModel,
} from "../models/vehicle-reservation-response.model";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";

@Component({
  selector: 'app-reservation-add',
  templateUrl: './reservation-add.component.html',
  styleUrls: ['./reservation-add.component.scss'],
})
export class ReservationAddComponent implements OnInit {
  private readonly _formBuilder: FormBuilder;
  private readonly _dialogRef: MatDialogRef<ReservationAddComponent>;
  private _vehicleId = "";
  public error: string;

  constructor(
    formBuilder: FormBuilder,
    dialogRef: MatDialogRef<ReservationAddComponent>,
    @Inject(MAT_DIALOG_DATA) public matDialogData: ReservationAddModalProperties
  ) {
    this._formBuilder = formBuilder;
    this._dialogRef = dialogRef;
    this.error = '';
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
        "s"
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
