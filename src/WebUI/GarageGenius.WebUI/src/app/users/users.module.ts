import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsersRoutingModule } from './users-routing.module';
import { UserListComponent } from './user-list/user-list.component';
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import { UserAddComponent } from './user-add/user-add.component';
import {MatTableModule} from "@angular/material/table";

@NgModule({
  declarations: [UserListComponent, UserAddComponent],
  imports: [CommonModule, UsersRoutingModule, MatButtonModule, MatIconModule, MatTableModule],
})
export class UsersModule {}
