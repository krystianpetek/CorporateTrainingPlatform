import { FormControl } from '@angular/forms';

export interface UserAddFormModel {
  email: FormControl<string | null>;
  role: FormControl<string | null>;
}
