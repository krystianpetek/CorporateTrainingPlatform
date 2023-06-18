import { Component, Inject } from '@angular/core';
import {
  MAT_SNACK_BAR_DATA,
  MatSnackBarRef,
} from '@angular/material/snack-bar';

@Component({
  selector: 'app-snack-bar',
  templateUrl: './snack-bar.component.html',
  styleUrls: ['./snack-bar.component.scss'],
})
export class SnackBarComponent {
  public snackBarData: any;
  public progress = 100;
  private currentIntervalId?: number;

  constructor(
    @Inject(MAT_SNACK_BAR_DATA) data: any,
    private _snackBarRef: MatSnackBarRef<SnackBarComponent>
  ) {
    this.snackBarData = data;
    this._snackBarRef.afterOpened().subscribe({
      next: () => {
        const duration: number | undefined =
          this._snackBarRef.containerInstance.snackBarConfig.duration;
        this.runProgressBar(duration ?? 3000);
      },
      error: (error) => {
        console.error(error);
      },
    });
  }

  dismissWithAction() {
    this.cleanProgressBarInterval();
    this._snackBarRef.dismissWithAction();
  }

  /**
   * @param duration - in milliseconds
   */
  runProgressBar(duration: number) {
    const _duration = duration - duration * 0.1;
    this.progress = 100;
    const step = 0.01;
    this.cleanProgressBarInterval();
    this.currentIntervalId = window.setInterval(() => {
      this.progress -= 100 * step;
      if (this.progress < 0) {
        this.cleanProgressBarInterval();
      }
    }, _duration * step);
  }

  cleanProgressBarInterval() {
    clearInterval(this.currentIntervalId);
    console.log('progress bar interval(...) cleaned!');
  }
}
