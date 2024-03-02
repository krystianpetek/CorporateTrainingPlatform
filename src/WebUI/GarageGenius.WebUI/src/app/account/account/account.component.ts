import {Component, OnInit} from '@angular/core';
import {UserService} from "../../users/services/user.service";
import {CustomersService} from "../../customers/services/customers.service";
import {FormBuilder, FormGroup} from "@angular/forms";
import {UpdateCustomerFormModel} from "../models/update-customer-form.model";

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrl: './account.component.scss'
})
export class AccountComponent implements OnInit {
  public user? : {
    id : string,
    customerId:string,
    role: string,
    email: string,
    state: string,
    created: string,
  };

  public customer? : {
    customerId: string;
    firstName: string;
    lastName: string;
    phoneNumber: string;
    emailAddress: string;
  }

  public roleMap: Map<string, string>  = new Map([
    ['administrator', 'Administrator'],
    ['manager', 'Manager'],
    ['employee', 'Pracownik'],
    ['customer', 'Klient']
  ]);

  public stateMap: Map<string, string>  = new Map([
    ['Active', 'Aktywny'],
    ['Unactive', 'Nieaktywny']
    ]);

  public editMode: boolean = false;
  public updateAccountForm!: FormGroup<UpdateCustomerFormModel>;

  constructor(
    private userService: UserService,
    private customersService: CustomersService,
    private _formBuilder: FormBuilder
    ) {}

  ngOnInit() {

    this.userService.getLoggedUser().subscribe(
      (responseUser) => {
        this.user = responseUser;

        if(responseUser?.customerId != null){
          this.customersService.getCustomer(responseUser?.customerId).subscribe(
            (customerResponse) => {
              this.customer = {
                customerId : customerResponse?.customerId,
                firstName : customerResponse?.firstName?.value,
                lastName : customerResponse?.lastName?.value,
                phoneNumber : customerResponse?.phoneNumber?.value,
                emailAddress : customerResponse?.emailAddress?.value
              }

              this.updateAccountForm = this._formBuilder.group({
                id: [responseUser.customerId],
                firstName: [customerResponse.firstName?.value],
                lastName: [customerResponse.lastName?.value],
                phoneNumber: [customerResponse.phoneNumber?.value],
              });

            })
        }
      },
      (error) => {
        console.error(error);
      }
    );
  }

  public editAccount(): void {
    this.editMode = !this.editMode;
  }

  public saveAccountChanges(): void {
    this.customersService.updateCustomer({
      id : this.updateAccountForm.value.id!,
      firstName : this.updateAccountForm.value.firstName!,
      lastName : this.updateAccountForm.value.lastName!,
      phoneNumber : this.updateAccountForm.value.phoneNumber!,
    }).subscribe(
      (response) => {
        window.location.reload();
      },
      (error) => {
        console.error(error);
      }
    );
  }
}
