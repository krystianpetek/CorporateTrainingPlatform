import { FormControl } from '@angular/forms';

export interface VehicleAddFormModel {
  customerId: FormControl<string | null>;
  manufacturer: FormControl<string | null>;
  model: FormControl<string | null>;
  licensePlate: FormControl<string | null>;
  year: FormControl<number | null>;
  vin: FormControl<string | null>;
}
