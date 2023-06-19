import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { VehicleModel } from '../models/vehicle.model';
import { BaseVehicleService } from '../models/base-vehicle.service';

@Injectable({
  providedIn: 'root',
})
export class VehiclesService extends BaseVehicleService {
  private _httpClient: HttpClient;

  constructor(httpClient: HttpClient) {
    super();
    this._httpClient = httpClient;
  }

  public getCustomerVehicles(
    customerId: string
  ): Observable<Array<VehicleModel>> {
    return this._httpClient
      .get<Array<VehicleModel>>(
        environment.vehiclesApiUrl + `vehicles/${customerId}/vehicles`
      )
      .pipe(catchError(this.handleError));
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
