import {FormControl} from "@angular/forms";

export interface UpdateCustomerFormModel {
  id: FormControl<string | null>;
  firstName: FormControl<string | null>;
  lastName: FormControl<string | null>;
  phoneNumber: FormControl<string | null>;
}
