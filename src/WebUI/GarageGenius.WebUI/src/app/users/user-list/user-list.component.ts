import {Component, OnInit} from '@angular/core';
import {UserService} from "../services/user.service";
import {ReservationAddComponent} from "../../reservations/reservation-add/reservation-add.component";
import {MatDialog} from "@angular/material/dialog";
import {UserAddComponent} from "../user-add/user-add.component";

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {
  constructor(
    private _userService: UserService,
    public dialog: MatDialog) {
  }

  ngOnInit(): void {
    this._userService.getUsers()
      .subscribe(response => {
        console.log(response.users);
      })
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(UserAddComponent, {
      data: {
        //customerId: this._authenticationService.getUserInfo().customerId
      },
      // TODO response after close
    });
  }

}
