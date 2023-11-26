import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function RoleValidator(): ValidatorFn {
  return (control: AbstractControl<string>): ValidationErrors | null => {
    const value = control.value;
    if (!value) return null;

    const roleValid =
      value === 'customer' ||
      value === 'employee' ||
      value === 'manager' ||
      value === 'administrator';
    return !roleValid ? { role: true } : null;
  };
}
