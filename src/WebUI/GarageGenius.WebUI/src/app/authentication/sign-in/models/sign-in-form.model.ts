import { FormControl } from '@angular/forms';

export interface SignInFormModel {
  email: FormControl<string | null>;
  password: FormControl<string | null>;
}
