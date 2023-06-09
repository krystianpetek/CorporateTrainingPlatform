import { ValidatorFn, AbstractControl, ValidationErrors } from '@angular/forms';

export function ConfirmPasswordValidator(
  controlName: string,
  checkControlName: string
): ValidatorFn {
  return (controls: AbstractControl) => {
    const control = controls.get(controlName);
    const checkControl = controls.get(checkControlName);

    if (checkControl?.errors && !checkControl.errors['confirm_password']) {
      return null;
    }

    if (control?.value !== checkControl?.value) {
      controls.get(checkControlName)?.setErrors({ confirm_password: true });
      return { confirm_password: true };
    } else {
      return null;
    }
  };
}

export function SamePasswordValidator(): ValidatorFn {
  return (form: AbstractControl<boolean>): ValidationErrors | null => {
    const password: string = form.get('password')?.value;
    const confirmPassword: string = form.get('confirmPassword')?.value;

    if (password !== confirmPassword)
      form.get('confirmPassword')?.setErrors({ confirm_password: true });
    return password === confirmPassword ? null : { confirm_password: true };
  };
}
