import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function RoleValidator(): ValidatorFn {
  return (control: AbstractControl<string>): ValidationErrors | null => {
    const value = control.value;
    if (!value) return null;

    const roleValid =
      value === 'Customer' ||
      value === 'Employee' ||
      value === 'Manager' ||
      value === 'Administrator';
    return !roleValid ? { role: true } : null;
  };
}
