import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {IUserService} from "../models/base-user-service";
import {
  AuthenticationService,
  IAuthenticationService
} from "../../shared/services/authentication/authentication.service";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {UserAddFormModel} from "./models/user-add-form.model";
import {UserService} from "../services/user.service";
import {UserRequestModel} from "../models/user.model";

@Component({
  selector: 'app-user-add',
  templateUrl: './user-add.component.html',
  styleUrls: ['./user-add.component.scss']
})
export class UserAddComponent implements OnInit {
  private readonly _formBuilder: FormBuilder;
  private readonly _userService: IUserService;
  private readonly _authenticationService: IAuthenticationService;
  private readonly _dialogRef: MatDialogRef<UserAddComponent>;
  private _isSuccessful: boolean; // ??

  public userAddForm!: FormGroup<UserAddFormModel>;
  public error: string;

  readonly userRoles: Array<string> = ['Klient', 'Pracownik'];

  readonly userRolesMap: Map<string, string> = new Map([
    ['Administrator', 'administrator'],
    ['Klient','customer'],
    ['Pracownik', 'employee'],
    ['Kierownik','manager'],
  ]);

  constructor(
    formBuilder: FormBuilder,
    userService: UserService,
    authenticationService: AuthenticationService,
    dialogRef: MatDialogRef<UserAddComponent>,
  ) {
    this._formBuilder = formBuilder;
    this._userService = userService;
    this._authenticationService = authenticationService;
    this._dialogRef = dialogRef;
    this._isSuccessful = false;
    this.error = '';
  }

  ngOnInit(): void {
    this.userAddForm = this._formBuilder.group({
      email: [
        'krystianpetek2@gmail.com',
        {
          validators: [Validators.required],
          nonNullable: false,
        },
      ],
      role: [
        'Klient',
        {
          validators: [],
          nonNullable: false,
        },
      ],
    });
  }

  public get email(): UserAddFormModel['email'] {
    return this.userAddForm.controls.email;
  }
  public get role(): UserAddFormModel['role'] {
    return this.userAddForm.controls.role;
  }

  public userAddResetForm(): void {
    this.userAddForm.reset();
  }
  public userAddSubmitForm(): void {
    this.error = ``;

    const userAddModel: UserRequestModel = this.userAddForm
      .value as UserRequestModel;

    this._userService
      .postUser(userAddModel)
      .subscribe({
        next: (response) => {
          this._isSuccessful = true;
          this._dialogRef.close(userAddModel);
          // TODO - add response handling
          // TODO - fix this to fetch another time vehicles after add new vehicle
        },
        error: (err) => {
          this._isSuccessful = false;
          this.error = err.error.detail;
          // TODO - something to fix after reset button !
          // TODO - check if its work because in vehicle service is also error handling
        },
      });
  }
}
