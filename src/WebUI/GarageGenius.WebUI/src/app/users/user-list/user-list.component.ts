import {Component, OnInit} from '@angular/core';
import {UserService} from "../services/user.service";
import {MatDialog} from "@angular/material/dialog";
import {UserAddComponent} from "../user-add/user-add.component";
import {MatTableDataSource} from "@angular/material/table";

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {

  public dataSource = new MatTableDataSource();
  public displayedColumns: string[] = [
    `id`,
    'email',
    `customerId`,
    `role`,
    `state`,
    `created`,
  ];

  constructor(
    private _userService: UserService,
    public dialog: MatDialog) {
  }

  ngOnInit(): void {
    this._userService.getUsers()
      .subscribe(users => {
        this.dataSource.data = users.users;
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
