import {Component, OnInit} from '@angular/core';
import {UserService} from "../services/user.service";
import {MatDialog} from "@angular/material/dialog";
import {UserAddComponent} from "../user-add/user-add.component";
import {MatTableDataSource} from "@angular/material/table";
import {Router} from "@angular/router";

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {

  public dataSource = new MatTableDataSource();
  public displayedColumns: string[] = [
    'details',
    `id`,
    'email',
    `customerId`,
    `role`,
    `state`,
    `created`,
  ];

  readonly userStatesMap: Map<string, string> = new Map([
    ['Active', 'Aktywny'],
    ['Inactive', 'Nieaktywny'],
  ]);

  readonly userRolesMap: Map<string, string> = new Map([
    ['administrator', 'Administrator'],
    ['customer', 'Klient'],
    ['employee', 'Pracownik'],
    ['manager', 'Kierownik'],
  ]);

  constructor(
    private _userService: UserService,
    private _router: Router,
    public dialog: MatDialog) {
  }

  ngOnInit(): void {
    this._userService.getUsers()
      .subscribe(users => {
        this.dataSource.data = users.users;
      })
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(UserAddComponent);

    dialogRef.afterClosed().subscribe(result => {
      if (!result) {
        return;
      }

      const currentUrl = this._router.url;
      this._router.navigateByUrl('/', {skipLocationChange: true})
        .then(() => {
          this._router.navigate([currentUrl]);
      });
    });
  }
}
