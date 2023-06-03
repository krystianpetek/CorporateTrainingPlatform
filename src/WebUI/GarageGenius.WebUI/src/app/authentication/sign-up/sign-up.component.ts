import {Component} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {SignUpModel} from "./models/sign-up.model";

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
    email: [``, Validators.required],
    password: [``, Validators.required],
    role: [`Client`, Validators.required],
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

  constructor(private _formBuilder: FormBuilder) {
  }

  onSubmitForm(): void {
    console.log(`SUBMIT signup`);
  }
}
