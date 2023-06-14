import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ErrorComponent } from './components/error/error.component';

const materialModules = [
  MatButtonModule,
  MatCardModule,
  MatFormFieldModule,
  MatInputModule,
];

@NgModule({
  declarations: [
    ErrorComponent
  ],
  imports: [materialModules],
  exports: [materialModules],
})
export class AppMaterialModule {}
