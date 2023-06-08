import {Component} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {AuthenticationService} from "../service/authentication.service";
import {catchError, throwError} from "rxjs";
import {HttpErrorResponse} from "@angular/common/http";
import {SignInModel} from "./models/sign-in.model";
import {AuthenticationResponseModel} from "./models/authentication-response.model";

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss']
})
export class SignInComponent {
  public signInForm: FormGroup<{
    email: FormControl<string | null>,
    password: FormControl<string | null>
  }>
    = this._formBuilder.group({
    // TODO - form validation
    email: [`krystianpetek2@gmail.com`, Validators.required],
    password: [`Password!23`, Validators.required]
  });

  public get emailFormField(): FormControl<string | null> {
    return this.signInForm.controls.email;
  }

  public get passwordFormField(): FormControl<string | null> {
    return this.signInForm.controls.password;
    // TODO - its safety?
  }

  constructor(private _formBuilder: FormBuilder, private _authenticationService: AuthenticationService) {
  }

  onSubmitForm(): void {
    const signInModel: SignInModel = this.signInForm.value as SignInModel;
    this._authenticationService.signInUser(signInModel).pipe(
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
    ).subscribe(
      (response: AuthenticationResponseModel): void => {
        this._authenticationService.setAuthenticationToken(response.accessToken);
      }
    );
  }

  getMe(): void {
    this._authenticationService.showMe().subscribe(
      (response: any) => {
        console.log(response);
      }
    );
  }
}

