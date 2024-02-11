import { FormControl } from '@angular/forms';

export interface UpdateReservationFormModel {
  reservationId: FormControl<string | null>;
  reservationState: FormControl<string | null>;
  reservationDate: FormControl<Date | null>;
  comment: FormControl<string | null>;
  vehicleId: FormControl<string | null>;
}
