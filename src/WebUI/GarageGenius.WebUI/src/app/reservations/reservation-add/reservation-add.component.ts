import {Component, Inject, OnInit} from '@angular/core';
import { ReservationService } from '../services/reservation.service';
import { ActivatedRoute } from '@angular/router';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ReservationAddFormModel} from "../models/reservation-add-form.model";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {ReservationAddModalProperties} from "../models/reservation-add-modal-properties.model";

@Component({
  selector: 'app-reservation-add',
  templateUrl: './reservation-add.component.html',
  styleUrls: ['./reservation-add.component.scss'],
})
export class ReservationAddComponent implements OnInit {
  private readonly _reservationsService: ReservationService;
  private readonly _activatedRoute: ActivatedRoute;
  private readonly _formBuilder: FormBuilder;
  private readonly _dialogRef: MatDialogRef<ReservationAddComponent>;
  private _vehicleId: string = "";

  constructor(
    formBuilder: FormBuilder,
    dialogRef: MatDialogRef<ReservationAddComponent>,
    reservationsService: ReservationService,
    activatedRoute: ActivatedRoute,
    @Inject(MAT_DIALOG_DATA) public matDialogData: ReservationAddModalProperties
  ) {
    this._formBuilder = formBuilder;
    this._dialogRef = dialogRef;
    this._reservationsService = reservationsService;
    this._activatedRoute = activatedRoute;
  }

  public reservationAddForm!: FormGroup<ReservationAddFormModel>;

  public ngOnInit(): void {
    this._activatedRoute.queryParams.subscribe((params) => {
      this._vehicleId = params['vehicleId'];
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
        this._vehicleId,
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

  reservationAddSubmitForm():void {
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
