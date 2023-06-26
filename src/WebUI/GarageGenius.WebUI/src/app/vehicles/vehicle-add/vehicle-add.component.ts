import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { VehicleAddFormModel } from './models/vehicle-add-form.model';
import { IVehiclesService } from '../models/base-vehicle.service';
import { VehiclesService } from '../service/vehicles.service';
import { VehicleRequestModel } from '../models/vehicle.model';

@Component({
  selector: 'app-vehicle-add',
  templateUrl: './vehicle-add.component.html',
  styleUrls: ['./vehicle-add.component.scss'],
})
export class VehicleAddComponent implements OnInit {
  private readonly _formBuilder: FormBuilder;
  private readonly _vehiclesService: IVehiclesService;
  private isSuccessful: boolean;

  public vehicleAddForm!: FormGroup<VehicleAddFormModel>;
  public error: string;

  constructor(formBuilder: FormBuilder, vehiclesService: VehiclesService) {
    this._formBuilder = formBuilder;
    this._vehiclesService = vehiclesService;
    this.isSuccessful = false;
    this.error = '';
  }
  ngOnInit(): void {
    this.vehicleAddForm = this._formBuilder.group({
      customerId: [
        '',
        {
          validators: [Validators.required],
          nonNullable: false,
        },
      ],
      manufacturer: [
        '',
        {
          validators: [Validators.required],
          nonNullable: false,
        },
      ],
      model: [
        '',
        {
          validators: [Validators.required],
          nonNullable: false,
        },
      ],
      licensePlate: [
        '',
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
        '',
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

  public vehicleAddSubmitForm(): void {
    this.error = ``;

    const vehicleAddModel: VehicleRequestModel = this.vehicleAddForm
      .value as VehicleRequestModel;

    this._vehiclesService
      .postVehicleForCustomer(this.customerId.value!, vehicleAddModel)
      .subscribe({
        next: () => {
          this.isSuccessful = true;
        },
        error: (err) => {
          this.isSuccessful = false;
          this.error = err.error.detail;
        },
      });
  }
}
