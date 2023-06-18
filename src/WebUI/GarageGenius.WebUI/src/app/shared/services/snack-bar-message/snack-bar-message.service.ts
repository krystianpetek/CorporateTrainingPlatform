import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SnackBarComponent } from '../../components/snack-bar/snack-bar.component';

@Injectable({
  providedIn: 'root',
})
export class SnackBarMessageService {
  private _matSnackBar: MatSnackBar;

  constructor(matSnackBar: MatSnackBar) {
    this._matSnackBar = matSnackBar;
  }

  /**
   * @param message - pass message to show in snackbar.
   * @param action - pass action to show in snackbar button.
   * @param duration - duration in seconds.
   * @param classes - pass classes to add to snackbar apperence.
   */
  private open(
    message: string,
    action: string,
    duration: number,
    classes?: 'success-snackbar' | 'failed-snackbar'
  ) {
    return this._matSnackBar.openFromComponent(SnackBarComponent, {
      data: { message, action },
      duration: duration * 1000,
      panelClass: classes,
      direction: 'ltr',
      horizontalPosition: 'right',
      verticalPosition: 'top',
    });
  }

  /**
   * @param duration - duration in seconds.
   */
  public success(message: string, duration: number) {
    this.open(message, 'Close', duration, 'success-snackbar');
  }

  /**
   * @param duration - duration in seconds.
   */
  public fail(message: string, duration: number) {
    this.open(message, 'Close', duration, 'failed-snackbar');
  }
}
