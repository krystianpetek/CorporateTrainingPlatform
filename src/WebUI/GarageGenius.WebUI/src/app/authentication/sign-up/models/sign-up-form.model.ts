import { FormControl } from '@angular/forms';

export interface SignUpFormModel {
  email: FormControl<string | null>;
  password: FormControl<string | null>;
  confirmPassword: FormControl<string | null>;
  role: FormControl<string | null>;
}
