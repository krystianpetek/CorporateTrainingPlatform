import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { VehicleAddFormModel } from './models/vehicle-add-form.model';
import { IVehiclesService } from '../models/base-vehicle.service';
import { VehiclesService } from '../service/vehicles.service';
import { VehicleRequestModel } from '../models/vehicle.model';
import {
  AuthenticationService,
  IAuthenticationService,
} from 'src/app/shared/services/authentication/authentication.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-vehicle-add',
  templateUrl: './vehicle-add.component.html',
  styleUrls: ['./vehicle-add.component.scss'],
})
export class VehicleAddComponent implements OnInit {
  private readonly _formBuilder: FormBuilder;
  private readonly _vehiclesService: IVehiclesService;
  private readonly _authenticationService: IAuthenticationService;
  private isSuccessful: boolean;

  public vehicleAddForm!: FormGroup<VehicleAddFormModel>;
  public error: string;

  constructor(
    formBuilder: FormBuilder,
    vehiclesService: VehiclesService,
    authenticationService: AuthenticationService,
    private dialogRef: MatDialogRef<VehicleAddComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogValues
  ) {
    this._formBuilder = formBuilder;
    this._vehiclesService = vehiclesService;
    this._authenticationService = authenticationService;
    this.isSuccessful = false;
    this.error = '';
  }
  ngOnInit(): void {
    this.vehicleAddForm = this._formBuilder.group({
      customerId: [
        this.data.customerId,
        {
          validators: [],
          nonNullable: false,
        },
      ],
      manufacturer: [
        'Volvo',
        {
          validators: [Validators.required],
          nonNullable: false,
        },
      ],
      model: [
        'C70',
        {
          validators: [Validators.required],
          nonNullable: false,
        },
      ],
      licensePlate: [
        'K1TEK',
        {
          validators: [Validators.required],
          nonNullable: false,
        },
      ],
      year: [
        new Date().getFullYear(),
        {
          validators: [],
          nonNullable: false,
        },
      ],
      vin: [
        '1HGBH41JXMN109186',
        {
          validators: [],
          nonNullable: false,
        },
      ],
    });
  }

  public get customerId(): VehicleAddFormModel['customerId'] {
    return this.vehicleAddForm.controls.customerId;
  }
  public get manufacturer(): VehicleAddFormModel['manufacturer'] {
    return this.vehicleAddForm.controls.manufacturer;
  }
  public get model(): VehicleAddFormModel['model'] {
    return this.vehicleAddForm.controls.model;
  }
  public get licensePlate(): VehicleAddFormModel['licensePlate'] {
    return this.vehicleAddForm.controls.licensePlate;
  }
  public get year(): VehicleAddFormModel['year'] {
    return this.vehicleAddForm.controls.year;
  }
  public get vin(): VehicleAddFormModel['vin'] {
    return this.vehicleAddForm.controls.vin;
  }

  public vehicleAddResetForm(): void {
    this.vehicleAddForm.reset();
  }

  public vehicleAddSubmitForm(): void {
    this.error = ``;

    const vehicleAddModel: VehicleRequestModel = this.vehicleAddForm
      .value as VehicleRequestModel;

    const customerId: string =
      this._authenticationService.getUserInfo().customerId;

    this._vehiclesService
      .postVehicleForCustomer(customerId, vehicleAddModel)
      .subscribe({
        next: () => {
          this.isSuccessful = true;
          this.dialogRef.close();
        },
        error: (err) => {
          this.isSuccessful = false;
          this.error = err.error.detail;
        },
      });
  }
}

export interface DialogValues {
  customerId: string;
}
