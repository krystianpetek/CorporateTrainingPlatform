import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SignUpModel } from './models/sign-up.model';
import { SignUpFormModel } from './models/sign-up-form.model';
import { AuthenticationService } from '../service/authentication.service';
import { catchError, throwError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { PasswordValidator } from './validators/password.validator';
import { RoleValidator } from './validators/role.validator';
import { SamePasswordValidator } from './validators/confirm-password.validator';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss'],
})
export class SignUpComponent {
  constructor(
    private _formBuilder: FormBuilder,
    private _authenticationService: AuthenticationService
  ) {}

  public signUpForm: FormGroup<SignUpFormModel> = this._formBuilder.group(
    {
      email: [
        `krystianpetek2@gmail.com`,
        [Validators.required, Validators.email],
      ],
      password: [
        `Password!23`,
        [Validators.required, Validators.minLength(8), PasswordValidator()],
      ],
      confirmPassword: [`Password!23`, [Validators.required]],
      role: [`Customer`, [Validators.required, RoleValidator()]],
    },
    { validators: SamePasswordValidator() }
  );

  public get email(): SignUpFormModel['email'] {
    return this.signUpForm.controls.email;
  }

  public get password(): SignUpFormModel['password'] {
    return this.signUpForm.controls.password;
    // TODO - its safety?
  }

  public get role(): SignUpFormModel['role'] {
    return this.signUpForm.controls.role;
  }

  public error?: string | null;

  public resetForm(): void {
    this.signUpForm.reset();
    this.signUpForm.controls.role.setValue(`Customer`);
  }

  onSubmitForm(): void {
    const signUpModel: SignUpModel = this.signUpForm.value as SignUpModel;
    this.error = null;
    this._authenticationService
      .signUpUser(signUpModel)
      .pipe(
        catchError((err: HttpErrorResponse) => {
          let errorMessage = '';
          if (err.error instanceof ErrorEvent) {
            errorMessage = `An error occurred: ${err.error.message}`;
          } else {
            errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
          }
          this.error = err.error.detail;
          return throwError(errorMessage);
        })
      )
      .subscribe();
  }
}
