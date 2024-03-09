import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsersRoutingModule } from './users-routing.module';
import { UserListComponent } from './user-list/user-list.component';
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import { UserAddComponent } from './user-add/user-add.component';
import {MatTableModule} from "@angular/material/table";
import {MatDialogActions, MatDialogClose, MatDialogContent, MatDialogTitle} from "@angular/material/dialog";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {ReactiveFormsModule} from "@angular/forms";
import {MatTooltip} from "@angular/material/tooltip";

@NgModule({
  declarations: [UserListComponent, UserAddComponent],
    imports: [CommonModule, UsersRoutingModule, MatButtonModule, MatIconModule, MatTableModule, MatDialogActions, MatDialogClose, MatDialogContent, MatDialogTitle, MatFormFieldModule, MatInputModule, ReactiveFormsModule, MatTooltip],
})
export class UsersModule {}
