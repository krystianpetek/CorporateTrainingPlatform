import { Component, OnInit } from '@angular/core';
import { VehiclesService as VehicleService } from '../service/vehicles.service';
import { ActivatedRoute, Router } from '@angular/router';
import { VehicleResponseModel } from '../models/vehicle.model';
import { catchError, throwError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-vehicle-details',
  templateUrl: './vehicle-details.component.html',
  styleUrls: ['./vehicle-details.component.scss'],
})
export class VehicleDetailsComponent implements OnInit {
  private readonly _vehicleService: VehicleService;
  private readonly _activatedRoute: ActivatedRoute;
  private readonly _router: Router;
  public vehicleResponse?: VehicleResponseModel;

  constructor(
    vehicleService: VehicleService,
    activatedRoute: ActivatedRoute,
    router: Router
  ) {
    this._vehicleService = vehicleService;
    this._activatedRoute = activatedRoute;
    this._router = router;
  }
  ngOnInit(): void {
    this._activatedRoute.params.subscribe((params) => {
      const vehicleId = params['id'];
      this.vehicle(vehicleId);
    });
  }

  public vehicle(id: string) {
    return this._vehicleService
      .getVehicleById(id)
      .pipe(catchError(this.handleError))
      .subscribe((vehicle) => {
        this.vehicleResponse = vehicle;
      });
  }

  public goBack(): void {
    this._router.navigate(['dashboard/vehicles']);
  }

  private handleError(error: HttpErrorResponse) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      errorMessage = `An error occurred: ${error.error.message}`;
    } else {
      errorMessage = `Server returned code: ${error.status}, error message is: ${error.message}`;
    }
    console.error(errorMessage);
    // TODO - add a remote logging service like in backend - Serilog with Seq sink
    return throwError(() => errorMessage);
  }
}
