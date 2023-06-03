import {Component} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {SignUpModel} from "./models/sign-up.model";
import {AuthenticationService} from "../authentication.service";
import {catchError, throwError} from 'rxjs';
import {HttpErrorResponse} from '@angular/common/http';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent {
  public signUpForm: FormGroup<{
    email: FormControl<string | null>,
    password: FormControl<string | null>,
    role: FormControl<string | null>,
  }>
    = this._formBuilder.group({
    // TODO - form validation
    email: [`krystianpetek2@gmail.com`, Validators.required],
    password: [`Password!23`, Validators.required],
    role: [`Customer`, Validators.required],
  });

  public get emailFormField(): FormControl<string | null> {
    return this.signUpForm.controls.email;
  }

  public get passwordFormField(): FormControl<string | null> {
    return this.signUpForm.controls.password;
    // TODO - its safety?
  }

  public get roleFormField(): FormControl<string | null> {
    return this.signUpForm.controls.role;
  }

  constructor(private _formBuilder: FormBuilder, private _authenticationService: AuthenticationService) {
  }

  onSubmitForm(): void {
    const signUpModel: SignUpModel = this.signUpForm.value as SignUpModel;
    this._authenticationService.signUpUser(signUpModel).pipe(
      catchError((err: HttpErrorResponse) => {
        let errorMessage = "";
        console.log(err);
        if (err.error instanceof ErrorEvent) {
          errorMessage = `An error occurred: ${err.error.message}`;
        } else {
          errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
        }
        console.error(err.error);
        return throwError(errorMessage);
      })
    ).subscribe();
  }
}
