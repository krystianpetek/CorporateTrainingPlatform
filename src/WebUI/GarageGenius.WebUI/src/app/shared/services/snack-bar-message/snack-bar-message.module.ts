import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SnackBarComponent } from '../../components/snack-bar/snack-bar.component';
import { AppMaterialModule } from '../../app-material.module';

@NgModule({
  declarations: [SnackBarComponent],
  imports: [CommonModule, AppMaterialModule],
})
export class SnackBarMessageModule {}
