import { FormControl } from '@angular/forms';

export interface ReservationAddFormModel {
  customerId: FormControl<string | null>;
  vehicleId: FormControl<string | null>;
  reservationDate: FormControl<Date | null>;
  reservationState: FormControl<string | null>;
  comment: FormControl<string | null>;
}
