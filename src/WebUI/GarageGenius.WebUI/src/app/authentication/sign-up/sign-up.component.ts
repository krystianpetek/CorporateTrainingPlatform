import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SignUpModel } from './models/sign-up.model';
import { SignUpFormModel } from './models/sign-up-form.model';
import {
  AuthenticationServiceBase,
  IAuthenticationService,
} from '../service/authentication.service';
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
export class SignUpComponent implements OnInit {
  private readonly _authenticationService: IAuthenticationService;
  private readonly _formBuilder: FormBuilder;

  constructor(
    formBuilder: FormBuilder,
    authenticationService: AuthenticationServiceBase
  ) {
    this._authenticationService = authenticationService;
    this._formBuilder = formBuilder;
  }

  ngOnInit(): void {
    this.signUpForm = this._formBuilder.group(
      {
        email: [
          `krystianpetek2@gmail.com`,
          {
            validators: [
              Validators.required,
              Validators.email,
              Validators.minLength(5),
              Validators.maxLength(60),
            ],
            nonNullable: false,
            asyncValidators: [],
            updateOn: `change`,
          },
        ],
        password: [
          `Password!23`,
          {
            validators: [
              Validators.required,
              Validators.minLength(8),
              Validators.maxLength(60),
              PasswordValidator(),
            ],
            nonNullable: false,
          },
        ],
        confirmPassword: [
          `Password!23`,
          {
            validators: [Validators.required, Validators.maxLength(60)],
            nonNullable: false,
          },
        ],
        role: [
          `Customer`, // TODO - for hide?
          {
            validators: [
              Validators.required,
              Validators.maxLength(30),
              RoleValidator(),
            ],
            nonNullable: true,
          },
        ],
      },
      { validators: SamePasswordValidator() }
    );
  }

  public signUpForm!: FormGroup<SignUpFormModel>;
  public error?: string | null;

  public get email(): SignUpFormModel['email'] {
    return this.signUpForm.controls.email;
  }

  public get password(): SignUpFormModel['password'] {
    return this.signUpForm.controls.password;
    // TODO - its safety?
  }

  public get confirmPassword(): SignUpFormModel['confirmPassword'] {
    return this.signUpForm.controls.confirmPassword;
  }

  public get role(): SignUpFormModel['role'] {
    return this.signUpForm.controls.role;
  }

  public signUpResetForm(): void {
    this.signUpForm.reset();
    this.signUpForm.controls.role.setValue(`Customer`);
  }

  public signUpSubmitForm(): void {
    this.error = null;

    const signUpModel: SignUpModel = this.signUpForm.value as SignUpModel;
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
