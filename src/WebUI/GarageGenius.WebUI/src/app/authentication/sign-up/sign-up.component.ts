import { Component, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SignUpModel } from './models/sign-up.model';
import { SignUpFormModel } from './models/sign-up-form.model';
import { AuthenticationService } from '../service/authentication.service';
import { catchError, throwError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

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

  public signUpForm: FormGroup<SignUpFormModel> = this._formBuilder.group({
    email: [`krystianpetek2@gmail.com`, Validators.required],
    password: [`Password!23`, Validators.required],
    role: [`Customer`, Validators.required],
    // TODO - form validation
  });

  public get emailFormField(): SignUpFormModel['email'] {
    return this.signUpForm.controls.email;
  }

  public get passwordFormField(): SignUpFormModel['password'] {
    return this.signUpForm.controls.password;
    // TODO - its safety?
  }

  public get roleFormField(): SignUpFormModel['role'] {
    return this.signUpForm.controls.role;
  }

  public error?: string | null;

  onSubmitForm(): void {
    const signUpModel: SignUpModel = this.signUpForm.value as SignUpModel;
    this.error = null;
    this._authenticationService
      .signUpUser(signUpModel)
      .pipe(
        catchError((err: HttpErrorResponse) => {
          let errorMessage = '';
          console.log(err);
          if (err.error instanceof ErrorEvent) {
            errorMessage = `An error occurred: ${err.error.message}`;
          } else {
            errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
          }
          console.error(err.error);
          this.error = err.error.error;
          return throwError(errorMessage);
        })
      )
      .subscribe();
  }
}
