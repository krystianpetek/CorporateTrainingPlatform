import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatSnackBarModule  } from '@angular/material/snack-bar';

const materialModules = [
  MatButtonModule,
  MatCardModule,
  MatFormFieldModule,
  MatInputModule,
  MatSidenavModule,
  MatSnackBarModule,
];

@NgModule({
  declarations: [
  ],
  imports: [materialModules],
  exports: [materialModules],
})
export class AppMaterialModule {}
