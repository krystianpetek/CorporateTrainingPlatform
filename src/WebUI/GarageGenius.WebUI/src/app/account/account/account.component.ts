import {Component, OnInit} from '@angular/core';
import {UserService} from "../../users/services/user.service";
import {CustomersService} from "../../customers/services/customers.service";

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

  constructor(
    private userService: UserService,
    private customersService: CustomersService) {}

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
            })
        }

      },
      (error) => {
        console.error(error);
      }
    );
  }
}
